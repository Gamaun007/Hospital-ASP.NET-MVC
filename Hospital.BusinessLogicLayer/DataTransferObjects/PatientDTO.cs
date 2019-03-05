using Hospital.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BusinessLogicLayer.DataTransferObjects
{
    public class PatientDTO
    {
        public int Id { set; get; }
        public bool IsDischarged { set; get; }
        public bool IsConfirmed { set; get; }

        public  UserProfile Profile { get; set; }
        public  MedicalCard MedicalCard { set; get; }
        public  Doctor Doctor { set; get; }
    }

}
