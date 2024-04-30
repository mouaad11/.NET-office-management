using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCabinetRepository.models;

namespace TestCabinetRepository.MySql
{
    public class PatientsController:ICRUD
    {
        private string connectionString;
        public PatientsController()
        {
            connectionString = "Server=localhost;Database=cabinet;Uid=root;Pwd=root;";
        }

        public bool Create(Object obj)
        {
            if (obj == null || obj is not Patient)
            {
                Console.WriteLine("L'objet passé en param est incompatible !");
                return false;
            }

            Patient p = (Patient)obj;
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("INSERT INTO patients (Nom, DateNaiss, Telephone, Cin) VALUES (@Nom, @DateNaiss, @Telephone, @Cin)", connection);
            command.Parameters.AddWithValue("@Nom", p.Nom);
            command.Parameters.AddWithValue("@DateNaiss", p.DateNaiss);
            command.Parameters.AddWithValue("@Telephone", p.Telephone);
            command.Parameters.AddWithValue("@Cin", p.Cin);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        public Object? Retrieve(string cin)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM patients WHERE Cin = @Cin", connection);
            command.Parameters.AddWithValue("@Cin", cin);
            using MySqlDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                Console.WriteLine("Patient Cin: " + cin + " n'existe pas !");
                return null;
            }
            Patient p = new Patient(reader.GetString(1), reader.GetDateTime(2), reader.GetString(3), reader.GetString(4));
            p.Id = reader.GetInt32(0);
            return p;
        }

        public Object? Retrieve(int id)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM patients WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            using MySqlDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                Console.WriteLine("Patient Id: " + id + " n'existe pas !");
                return null;
            }
            Patient p = new Patient(reader.GetString(1), reader.GetDateTime(2), reader.GetString(3), reader.GetString(4));
            p.Id = reader.GetInt32(0);
            return p;
        }


        public bool Update(Object obj)
        {
            if (obj == null || obj is not Patient)
            {
                Console.WriteLine("L'objet passé en paramètre est incompatible !");
                return false;
            }

            Patient updatedPatient = (Patient)obj;

            using var connection = new MySqlConnection("Server=localhost;Database=cabinet;Uid=root;Pwd=root;");
            connection.Open();

            using var command = new MySqlCommand("UPDATE patients SET Nom=@Nom, DateNaiss=@DateNaiss, Cin=@Cin, Telephone=@Telephone WHERE Id=@Id", connection);
            command.Parameters.AddWithValue("@Nom", updatedPatient.Nom);
            command.Parameters.AddWithValue("@DateNaiss", updatedPatient.DateNaiss);
            command.Parameters.AddWithValue("@Cin", updatedPatient.Cin);
            command.Parameters.AddWithValue("@Telephone", updatedPatient.Telephone);
            command.Parameters.AddWithValue("@Id", updatedPatient.Id);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                Console.WriteLine("Patient Id: " + updatedPatient.Id + " n'existe pas !");
                return false;
            }

            Console.WriteLine("Patient Id: " + updatedPatient.Id + " a été modifié avec succès !");
            return true;
        }


        public bool Delete(int id)
        {
            using var connection = new MySqlConnection("Server=localhost;Database=cabinet;Uid=root;Pwd=root;");
            connection.Open();

            using var command = new MySqlCommand("DELETE FROM patients WHERE Id=@Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                Console.WriteLine("Patient Id: " + id + " n'existe pas !");
                return false;
            }

            Console.WriteLine("Patient Id: " + id + " a été supprimé avec succès !");
            return true;
        }

        public bool Delete(string cin)
        {
            using var connection = new MySqlConnection("Server=localhost;Database=cabinet;Uid=root;Pwd=root;");
            connection.Open();

            using var command = new MySqlCommand("DELETE FROM patients WHERE Cin=@Cin", connection);
            command.Parameters.AddWithValue("@Cin", cin);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected == 0)
            {
                Console.WriteLine("Patient Cin: " + cin + " n'existe pas !");
                return false;
            }

            Console.WriteLine("Patient Cin: " + cin + " a été supprimé avec succès !");
            return true;
        }


    }
}
