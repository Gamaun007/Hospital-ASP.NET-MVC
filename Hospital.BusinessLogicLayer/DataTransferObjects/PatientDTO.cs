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
        public string PhoneNumber { set; get; }
        public string AdditionalInformation { set; get; }

        public  ProfileDTO Profile { get; set; }
        public  MedicalCardDTO MedicalCard { set; get; }
        public  DoctorDTO Doctor { set; get; }
    }

}
