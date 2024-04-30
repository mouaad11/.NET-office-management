using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCabinetRepository.models;
using MySqlConnector;

namespace TestCabinetRepository.MySql
{
    internal class RendezVousController:ICRUD
    {
        private string connectionString;
        private MySqlConnection connection;

        public RendezVousController()
        {
            connectionString = "Server=localhost;Database=cabinet;Uid=root;Pwd=root;";

        }

        public bool Create(Object obj)
        {
            if (obj == null || obj is not RendezVous)
            {
                Console.WriteLine("L'objet passé en param est incompatible !");
                return false;
            }

            RendezVous r = (RendezVous)obj;

            string query = $"INSERT INTO rendezvous (patientId, dateRDV, heurRDV) VALUES ({r.PatientId}, '{r.DateRDV.ToString("yyyy-MM-dd")}', '{r.HeurRDV.ToString(@"hh\:mm\:ss")}')";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    return true;
                }
            }

            Console.WriteLine("RendezVous Id: " + r.Id + " existe déjà !");
            return false;
        }

        public Object? Retrieve(string cin)
        {
            string query = $"SELECT * FROM rendezvous WHERE patientId = {cin}";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        RendezVous r = new RendezVous(
                            reader.GetInt32("patientId"),
                            reader.GetDateTime("dateRDV"),
                            reader.GetTimeSpan("heurRDV")
                        );

                        return r;
                    }
                }
            }

            Console.WriteLine("Patient Cin: " + cin + " n'existe pas !");
            return null;
        }

        public Object? Retrieve(int patientId)
        {
            string query = $"SELECT * FROM rendezvous WHERE patientId = {patientId}";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        RendezVous r = new RendezVous(
                            reader.GetInt32("patientId"),
                            reader.GetDateTime("dateRDV"),
                            reader.GetTimeSpan("heurRDV")
                        );

                        return r;
                    }
                }
            }

            Console.WriteLine("Patient Id: " + patientId + " n'existe pas !");
            return null;
        }

        public bool Update(Object? obj)
        {
            if (obj == null || obj is not RendezVous)
            {
                Console.WriteLine("L'objet passé en param est incompatible !");
                return false;
            }

            RendezVous newRendezVous = (RendezVous)obj;

            string query = $"UPDATE rendezvous SET dateRDV = '{newRendezVous.DateRDV.ToString("yyyy-MM-dd")}', heurRDV = '{newRendezVous.HeurRDV.ToString(@"hh\:mm\:ss")}' WHERE patientId = {newRendezVous.PatientId}";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                int result = command.ExecuteNonQuery();

                if (result > 0)
                {
                    return true;
                }
            }

            Console.WriteLine("RendezVous Id: " + newRendezVous.Id + " introuvable!");
            return false;
        }

        public bool Delete(string cin)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=cabinet;Uid=root;Pwd=root;"))
            {
                try
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM rendezvous WHERE cin = @cin";
                    cmd.Parameters.AddWithValue("@cin", cin);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        Console.WriteLine("RendezVous Id: " + cin + " introuvable !");
                        return false;
                    }

                    Console.WriteLine("RendezVous Id: " + cin + " supprimé avec succès !");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la suppression du rendez-vous: " + ex.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        public bool Delete(int id)
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=cabinet;Uid=root;Pwd=root;"))
            {
                try
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM rendezvous WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        Console.WriteLine("RendezVous Id: " + id + " introuvable !");
                        return false;
                    }

                    Console.WriteLine("RendezVous Id: " + id + " supprimé avec succès !");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de la suppression du rendez-vous: " + ex.Message);
                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
