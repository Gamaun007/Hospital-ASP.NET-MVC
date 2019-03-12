using Hospital.DataAccessLayer.Entities;
using Hospital.DataAccessLayer.Entities.ExtraModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BusinessLogicLayer.DataTransferObjects
{
    public class MedicalCardDTO
    {
        public int Id { set; get; }
        public DateTime CreatingDate { set; get; }

        public ICollection<MedicalCardPageDTO> Pages { get; set; }
        public PatientDTO Patient { set; get; }
    }
}
