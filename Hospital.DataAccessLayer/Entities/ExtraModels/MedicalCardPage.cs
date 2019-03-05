using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DataAccessLayer.Entities.ExtraModels
{
    public class MedicalCardPage
    {
        [Key]
        public int Id { set; get; }
        public DateTime NotationTime { get; set; }
        public virtual Patient Patient { get; set; }

        public string Procedures { get; set; }
        public string Medicaments { get; set; }
        public string SurgeryOperation { get; set; }
        public string DIagnosis { get; set; }
    }
}
