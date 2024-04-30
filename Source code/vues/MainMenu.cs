using TestCabinetRepository.models;
using TestCabinetRepository.vues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Mime;
using System.Windows.Forms;


namespace TestCabinetRepository.vues
{
    internal class MainMenu
    {
        private PatientMenu _patientMenu;
        private RendezVousMenu _rendezVousMenu;

        public MainMenu() {
            _patientMenu = new();
            _rendezVousMenu = new();
        }
        public void Show()
        {
            string? choix;
            do
            {
                Console.WriteLine("### Main Menu ###");
                Console.WriteLine("-- 1: Patients");
                Console.WriteLine("-- 2: Salle d'attente");
                Console.WriteLine("-- 3: Rendez-vous");
                Console.WriteLine("-- 4: Consultation");
                Console.WriteLine("-- q: Quit");
                choix = Console.ReadLine();
                Console.WriteLine("\n\n\n\n");

                switch (choix)
                {
                    case "1":
                        _patientMenu.Show();
                        break;
                    case "2":
                        Console.WriteLine("Not implemented yet !");
                        break;
                    case "3":
                        _rendezVousMenu.Show();
                        break;
                    case "4":
                        Console.WriteLine("Not implemented yet !");
                        break;
                }

            } while (choix != "q");
        }
    }


}
