using Hospital.DataAccessLayer.Entities;
using Hospital.DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BusinessLogicLayer.DataTransferObjects
{
    public class DoctorDTO
    {
        public int Id { set; get; }
        public string PhoneNumber { set; get; }
        public string Specialization { get; set; }
        public string AdditionalInformation { set; get; }
        public ICollection<Patient> Patients { get; set; }
        public UserProfile Profile { get; set; }
        public bool IsConfirmed { set; get; }
    }
}
