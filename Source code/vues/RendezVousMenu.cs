using TestCabinetRepository.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCabinetRepository.models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using MySqlConnector;

namespace TestCabinetRepository.vues
{
    internal class RendezVousMenu
    {
        RendezVousController _rendezVousController;
        private string? patientCin;

        public RendezVousMenu() 
        { 
            _rendezVousController = new RendezVousController(); 
        }
        public void Show()
        {
            string? choix;
            do {
                Console.WriteLine("---> ##### Menu Rendez Vous ##### <");
                Console.WriteLine("--- 1: Afficher touts les rendez vous");
                Console.WriteLine("--- 2: Créer rendez vous");
                Console.WriteLine("--- 3: Supprimer rendez vous par Id patient");
                Console.WriteLine("--- 4: Supprimer rendez vous par CIN patient");
                Console.WriteLine("--- 5: Chercher rendez vous par Id patient");
                Console.WriteLine("--- 6: Chercher rendez vous par CIN patient");
                Console.WriteLine("--- 7: Mise à jour rendez vous");
                Console.WriteLine("--- q: Quitter");
                choix = Console.ReadLine();
                Console.WriteLine("\n\n\n\n");

                // bool to recieve the result of CRUD operations
                bool isDone = false;
                var isInt = false;
                int id;
                int patientId;
                RendezVous? RendezVous;

                switch (choix){
                    case "1":
                        string connectionString = "server=localhost;database=cabinet;uid=root;password=root;";

                        MySqlConnection connection = new MySqlConnection(connectionString);
                        connection.Open();

                        MySqlCommand command = new MySqlCommand("SELECT * FROM rendezvous", connection);
                        MySqlDataReader reader = command.ExecuteReader();

                        Console.WriteLine("------ Liste Rendez Vous :-");
                        while (reader.Read())
                        {
                            int idVal = reader.GetInt32("id");
                            int patientIdVal = reader.GetInt32("patientId");
                            DateTime dateRDVVal = reader.GetDateTime("dateRDV");
                            DateTime heurRDVVal = reader.GetDateTime("heurRDV");

                            Console.WriteLine($"Id: {idVal}, Patient Id: {patientIdVal}, Date Rendez Vous: {dateRDVVal}, Heur Rendez Vous: {heurRDVVal}");
                        }

                        reader.Close();
                        connection.Close();
                        break;
                    case "2":
                        Console.WriteLine("------ Ajouter Rendez Vous :-");
                        string patientIdStr = Console.ReadLine();
                      
                        while (!int.TryParse(patientIdStr, out patientId))
                        {
                            Console.Write("Veuillez entrer un nombre entier : ");
                            patientIdStr = Console.ReadLine();
                        }

                        DateTime dateRDV;
                        TimeSpan heurRDV;
                        bool isValideDate = false;
                        bool isValideTime = false;
                        do
                        {
                             Console.Write("Date Rendez Vous : ");
                             isValideDate = DateTime.TryParse(Console.ReadLine(), out dateRDV);
                        } while (!isValideDate);

                        do
                        {
                         Console.Write("Horraire Rendez Vous : ");
                         isValideTime = TimeSpan.TryParse(Console.ReadLine(), out heurRDV);
                        } while (!isValideTime);

                        isDone = _rendezVousController.Create(new RendezVous(patientId, dateRDV, heurRDV));
                        if (isDone)
                        {
                            Console.WriteLine("RendezVous : Ajouté avec Success !");
                        }
                        else
                        {
                            Console.WriteLine("RendezVous : problème d'ajout !");
                        }
                    break;

                    case "3":

                        Console.WriteLine("------ Supprimer RendezVous par Id patient :-");
                        
                        do
                        {
                            Console.WriteLine("Id patient: ");
                            isInt = int.TryParse(Console.ReadLine(), out patientId);
                        } while (!isInt);
                        
                        isDone = _rendezVousController.Delete(patientId);
                        if (isDone)
                        {
                            Console.WriteLine("RendezVous : supprimé avec Success !");
                        }
                        else
                        {
                            Console.WriteLine("RendezVous : problème de supression !");
                        }

                        break;
                        case "4":
                        Console.WriteLine("------ Supprimer rendez vous par CIN patient :-");

                        Console.WriteLine("Patient Cin: ");
                        patientCin = Console.ReadLine();

                        isDone = _rendezVousController.Delete(patientCin);
                        if (isDone)
                        {
                            Console.WriteLine("Patient : supprimé avec Success !");
                        }
                        else
                        {
                            Console.WriteLine("Patient : problème de supression !");
                        }
                        break;


                    case "5":

                        Console.WriteLine("------ Chercher Rendez Vous by Id patient :-");
                        
                        Console.WriteLine("Patient Id: ");
                        patientId = int.Parse(Console.ReadLine());

                        object obj  = _rendezVousController.Retrieve(patientId);
                        if (obj != null)
                        {
                            RendezVous r = (RendezVous)obj;
                            Console.WriteLine(r);
                        }
                        else
                        {
                            Console.WriteLine("RendezVous : introuvable !");
                        }

                        break;

                        case "6":
                        Console.WriteLine("------ Chercher Patient par CIN :-");
                        
                        Console.WriteLine("Patient Cin: ");
                        patientCin = Console.ReadLine();

                        object obj2  = _rendezVousController.Retrieve(patientCin);
                        if (obj2 != null)
                        {
                            Patient p = (Patient)obj2;
                            Console.WriteLine(p);
                        }
                        else
                        {
                            Console.WriteLine("Patient : introuvable !");
                        }
                        break;
                    case "7":

                        Console.WriteLine("------ Mise à jour Rendez Vous :-");

                        do
                        {
                            Console.WriteLine("Id patient: ");
                            isInt = int.TryParse(Console.ReadLine(), out patientId);
                        } while (!isInt);

                        obj = _rendezVousController.Retrieve(patientId);
                        if (obj != null)
                        {
                            RendezVous = (RendezVous)obj;
                            Console.WriteLine(RendezVous);
                            string choix2;
                            do
                            {
                                Console.WriteLine("Choisir l'attribut à modifier : ");
                                Console.WriteLine("1... patientId");
                                Console.WriteLine("2... Date Rendez vous");
                                Console.WriteLine("3... Horraire Rendez vous");
                                Console.WriteLine("q... quit");
                                choix2 = Console.ReadLine();
                                Console.WriteLine("\n\n\n\n");

                                switch (choix2)
                                {
                                    case "1":
                                        Console.Write("Nouveau patientId: ");
                                        patientId = int.Parse(Console.ReadLine());
                                        RendezVous.PatientId = patientId;
                                        isDone = _rendezVousController.Update(RendezVous);
                                        if (isDone)
                                        {
                                            Console.WriteLine("RendezVous : Modifié avec Success !");
                                        }
                                        else
                                        {
                                            Console.WriteLine("RendezVous : problème de modification !");
                                        }
                                        break;
                                    case "2":
                                        do
                                        {
                                            Console.Write("Nouvelle Date de Rendez vous: ");
                                            isValideDate = DateTime.TryParse(Console.ReadLine(), out dateRDV);
                                        } while (!isValideDate);
                                        isDone = _rendezVousController.Update(RendezVous);
                                        if (isDone)
                                        {
                                            Console.WriteLine("RendezVous : Modifié avec Success !");
                                        }
                                        else
                                        {
                                            Console.WriteLine("RendezVous : problème de modification !");
                                        }
                                        break;

                                        case "3":
                                        do
                                        {
                                            Console.Write("Nouveau horraire de Rendez vous: ");
                                            isValideDate = TimeSpan.TryParse(Console.ReadLine(), out heurRDV);
                                        } while (!isValideDate);
                                        isDone = _rendezVousController.Update(RendezVous);
                                        if (isDone)
                                        {
                                            Console.WriteLine("RendezVous : Modifié avec Success !");
                                        }
                                        else
                                        {
                                            Console.WriteLine("RendezVous : problème de modification !");
                                        }
                                        break;
                                }
                            } while (choix2 != "q");
                        }
                        else
                        {
                            Console.WriteLine("RendezVous : introuvable !");
                        }

                        break;
                }

            } while (choix!="q");
        }
    }
}
