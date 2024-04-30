using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCabinetRepository.models
{
    internal class Consultation
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string ConsultationType { get; set; }
        public decimal Prix { get; set; }
        public string ConsultationDate { get; set; }
        public string Description { get; set; }
        public string Cin { get; internal set; }

       
        internal int patientId;
        internal string? consultationType;
        internal decimal? prix;

        public Consultation(int patientId, string consultationType, int prix)
        {
            PatientId = patientId;
            ConsultationType = consultationType;
            Prix = prix;
            
        }

        public override string ToString()
        {
            return "Consultation{" +
                    "Id=" + Id +
                    ", Patient_Id=" + PatientId +
                    ", ConsultationType='" + ConsultationType + '\'' +
                    ", Prix=" + Prix +
                    ", Date=" + ConsultationDate +
                    '}';
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Consultation consultation = (Consultation) obj;
            return Id == consultation.Id && PatientId == consultation.PatientId;
        }

    }
}
