using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCabinetRepository.models
{
    internal class RendezVous
    {


        public int Id { get; set; }

        public int PatientId { get; set; }

        public DateTime DateRDV { get; set; }

        public TimeSpan HeurRDV { get; set; }
        public string Cin { get; internal set; }

        
        internal int patientId;

        public RendezVous(int patientId, DateTime dateRDV, TimeSpan heurRDV)
        {
            PatientId = patientId;
            DateRDV = dateRDV;
            HeurRDV = heurRDV;
            
        }

        public override string ToString()
        {
            return "RendezVous{" +
                    "Id=" + Id +
                    ", Patient_Id=" + PatientId +
                    ", DateRDV=" + DateRDV +
                    ", HeurRDV=" + HeurRDV +
                    '}';
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            RendezVous rendezVous = (RendezVous) obj;
            return Id == rendezVous.Id &&
                    PatientId == rendezVous.PatientId &&
                    DateRDV.Equals(rendezVous.DateRDV) &&
                    HeurRDV.Equals(rendezVous.HeurRDV);
        }

    }
}
