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
    internal class PatientMenu
    {
        PatientsController _patientController;

        public PatientMenu() 
        { 
            _patientController = new PatientsController(); 
        }
        public void Show()
        {
            string? choix;
            do {
                Console.WriteLine("---> Patient Menu <");
                Console.WriteLine("--- 1: Show All Patients");
                Console.WriteLine("--- 2: Create Patient");
                Console.WriteLine("--- 3: Delete Patient by Id");
                Console.WriteLine("--- 4: Delete Patient by Cin");
                Console.WriteLine("--- 5: Retrieve Patient by Id");
                Console.WriteLine("--- 6: Retrieve Patient by Cin");
                Console.WriteLine("--- 7: Update Patient");
                Console.WriteLine("--- q: Quit");
                choix = Console.ReadLine();
                Console.WriteLine("\n\n\n\n");

                // bool to recieve the result of CRUD operations
                bool isDone = false;
                var isInt = false;
                int patientId;
                string patientCin;
                Patient? patient;

                switch (choix){
                    case "1":
                        string connectionString = "server=localhost;database=cabinet;uid=root;password=root;";

                        MySqlConnection connection = new MySqlConnection(connectionString);
                        connection.Open();

                        MySqlCommand command = new MySqlCommand("SELECT * FROM patients", connection);
                        MySqlDataReader reader = command.ExecuteReader();

                        Console.WriteLine("------ Liste Patients :-");
                        while (reader.Read())
                        {
                            string nomVal = reader.GetString("nom");
                            DateTime dateNaissVal = reader.GetDateTime("dateNaiss");
                            int telephoneVal = reader.GetInt32("telephone");
                            string cinVal = reader.GetString("cin");

                            Console.WriteLine($"Nom: {nomVal}, Date de Naissance: {dateNaissVal}, Téléphone: {telephoneVal}, CIN: {cinVal}");
                        }

                        reader.Close();
                        connection.Close();
                        break;
                    case "2":
                        Console.WriteLine("------ Add patient :-");
                        Console.Write("Nom : ");
                        string? nom = Console.ReadLine();
                        Console.Write("Phone : ");
                        string? telephone = Console.ReadLine();
                        Console.Write("Cin : ");
                        string? cin = Console.ReadLine();
                        DateTime dateNaiss;
                        bool isValideDate = false;
                        do
                        {
                            Console.Write("Date Naissance : ");
                            isValideDate = DateTime.TryParse(Console.ReadLine(), out dateNaiss);
                        } while (!isValideDate);

                        isDone = _patientController.Create(new Patient(nom, dateNaiss, telephone, cin));
                        if (isDone)
                        {
                            Console.WriteLine("Patient : Ajouté avec Success !");
                        }
                        else
                        {
                            Console.WriteLine("Patient : problème d'ajout !");
                        }
                        break;
                    case "3":

                        Console.WriteLine("------ Delete Patient by ID :-");
                        
                        do
                        {
                            Console.WriteLine("Patient Id: ");
                            isInt = int.TryParse(Console.ReadLine(), out patientId);
                        } while (!isInt);
                        
                        isDone = _patientController.Delete(patientId);
                        if (isDone)
                        {
                            Console.WriteLine("Patient : supprimé avec Success !");
                        }
                        else
                        {
                            Console.WriteLine("Patient : problème de supression !");
                        }

                        break;
                    case "4":

                        Console.WriteLine("------ Delete Patient by Cin :-");

                        Console.WriteLine("Patient Cin: ");
                        patientCin = Console.ReadLine();

                        isDone = _patientController.Delete(patientCin);
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

                        Console.WriteLine("------ Search Patient by ID :-");

                        do
                        {
                            Console.WriteLine("Patient Id: ");
                            isInt = int.TryParse(Console.ReadLine(), out patientId);
                        } while (!isInt);

                        patient = (Patient) _patientController.Retrieve(patientId);
                        if (patient != null)
                        {
                            Console.WriteLine(patient);
                        }
                        else
                        {
                            Console.WriteLine("Patient : introuvable !");
                        }

                        break;
                    case "6":

                        Console.WriteLine("------ Search Patient by Cin :-");
                        
                        Console.WriteLine("Patient Cin: ");
                        patientCin = Console.ReadLine();

                        object obj  = _patientController.Retrieve(patientCin);
                        if (obj != null)
                        {
                            Patient p = (Patient)obj;
                            Console.WriteLine(p);
                        }
                        else
                        {
                            Console.WriteLine("Patient : introuvable !");
                        }

                        break;
                    case "7":

                        Console.WriteLine("------ Update Patient :-");

                        do
                        {
                            Console.WriteLine("Patient Id: ");
                            isInt = int.TryParse(Console.ReadLine(), out patientId);
                        } while (!isInt);

                        obj = _patientController.Retrieve(patientId);
                        if (obj != null)
                        {
                            patient = (Patient)obj;
                            Console.WriteLine(patient);
                            string choix2;
                            do
                            {
                                Console.WriteLine("Choisir l'attribut à modifier : ");
                                Console.WriteLine("1... Nom");
                                Console.WriteLine("2... Cin");
                                Console.WriteLine("3... Date Naissance");
                                Console.WriteLine("q... quit");
                                choix2 = Console.ReadLine();
                                Console.WriteLine("\n\n\n\n");

                                switch (choix2)
                                {
                                    case "1":
                                        Console.Write("Nouveau Nom: ");
                                        nom = Console.ReadLine();
                                        patient.Nom = nom;
                                        isDone = _patientController.Update(patient);
                                        if (isDone)
                                        {
                                            Console.WriteLine("Patient : Modifié avec Success !");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Patient : problème de modification !");
                                        }
                                        break;
                                    case "2":
                                        Console.Write("Nouveau Cin: ");
                                        cin = Console.ReadLine();
                                        patient.Cin = cin;
                                        isDone = _patientController.Update(patient);
                                        if (isDone)
                                        {
                                            Console.WriteLine("Patient : Modifié avec Success !");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Patient : problème de modification !");
                                        }
                                        break;
                                    case "3":
                                        do
                                        {
                                            Console.Write("Nouvelle Date de naissance: ");
                                            isValideDate = DateTime.TryParse(Console.ReadLine(), out dateNaiss);
                                        } while (!isValideDate);
                                        isDone = _patientController.Update(patient);
                                        if (isDone)
                                        {
                                            Console.WriteLine("Patient : Modifié avec Success !");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Patient : problème de modification !");
                                        }
                                        break;
                                }
                            } while (choix2 != "q");
                        }
                        else
                        {
                            Console.WriteLine("Patient : introuvable !");
                        }

                        break;
                }

            } while (choix!="q");
        }
    }
}
