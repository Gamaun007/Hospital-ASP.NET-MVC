using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BusinessLogicLayer.DataTransferObjects
{
    public class MedicalCardPageDTO
    {
        
        public int Id { set; get; }
        public DateTime NotationTime { get; set; }
        public MedicalCardDTO MedicalCard { get; set; }

        public string Procedures { get; set; }
        public string Medicaments { get; set; }
        public string SurgeryOperation { get; set; }
        public string Diagnosis { get; set; }
    }
}
