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
        public Specialization Specialization { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public UserProfile Profile { get; set; }
        public bool IsConfirmed { set; get; }
    }
}
