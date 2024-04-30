using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCabinetRepository.models
{
    public class Patient:Personne
    {
        public DateTime DateNaiss { get; set; }
        public string Telephone { get; set; }
        public string Cin { get; set; }

        public Patient(string nom, DateTime dateNaiss, string telephone, string cin):base(nom)
        {
            DateNaiss = dateNaiss;
            Telephone = telephone;
            Cin = cin;
        }

        public override string ToString()
        {
            return base.ToString() + " , " +
                    ", CIN='" + Cin + " , " +
                    ", Age=" + (int)((DateTime.Now -DateNaiss).Days / 365.25) + " ," +
                    ", Tele='" + Telephone;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Patient patient = (Patient) obj;
            return (Cin.Equals(patient.Cin));
        }

    }

}
