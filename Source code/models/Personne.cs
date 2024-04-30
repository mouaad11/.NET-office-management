using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCabinetRepository.models
{
    public abstract class Personne
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public DateTime LastUpdated { get; set; }

        private static Random _randomId = new Random();
        public Personne(string nom)
        {
            Nom = nom;
            LastUpdated = DateTime.Now;
            //string uniqueString = DateTime.Now.ToString("yyyyMMddHHmmss");// + new Random().Next(100);
            string uniqueId = DateTime.Now.ToString("yyMMdd") + _randomId.Next(100,999);
            Id=int.Parse(uniqueId);
        }

        public override string ToString()
        {
            return "Id: " + Id + " , " +
                "Nom: " + Nom;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Personne personne = (Personne)obj;
            return (Id == personne.Id && Nom.Equals(personne.Nom));
        }

    }
}
