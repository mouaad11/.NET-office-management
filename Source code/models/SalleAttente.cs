using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCabinetRepository.models
{
    internal class SalleAttente
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime DateVisit { get; set; }
        public int VisitOrder { get; set; }
        public string Cin { get; internal set; }

        
        public SalleAttente(int patientId, DateTime dateVisit, int visitOrder)
        {
            PatientId = patientId;
            DateVisit = dateVisit;
            VisitOrder = visitOrder;
            
        }

        public override string ToString()
        {
            return "SalleAttente{" +
                    "Id=" + Id +
                    ", Patient_Id=" + PatientId +
                    ", DateVisit=" + DateVisit +
                    ", VisitOrder=" + VisitOrder +
                    '}';
        }

        public override bool Equals(object? o)
        {
            if (o == null || GetType() != o.GetType()) return false;
            SalleAttente salleAttente = (SalleAttente)o;
            return Id == salleAttente.Id &&
                    PatientId == salleAttente.PatientId &&
                    DateVisit.Equals(salleAttente.DateVisit) &&
                    VisitOrder == salleAttente.VisitOrder;
        }
    }
}
