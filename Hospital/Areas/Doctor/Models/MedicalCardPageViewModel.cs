using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Areas.Doctor.Models
{
    public class MedicalCardPageViewModel
    {
        public int Id { set; get; }
        public DateTime NotationTime { get; set; }
       // public MedicacalCard 
        public string Procedures { get; set; }
        public string Medicaments { get; set; }
        public string SurgeryOperation { get; set; }
        public string Diagnosis { get; set; }
    }
}