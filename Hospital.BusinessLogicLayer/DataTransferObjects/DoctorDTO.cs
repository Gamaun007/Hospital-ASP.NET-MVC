using Hospital.DataAccessLayer.Entities;
using Hospital.DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BusinessLogicLayer.DataTransferObjects
{
    public class DoctorDTO
    {
        public int Id { set; get; }
        //[Display(Name ="Phone Number")]
        public string PhoneNumber { set; get; }
        public string Specialization { get; set; }
        //[Display(Name = "Additional Info.")]
        public string AdditionalInformation { set; get; }
        public ICollection<PatientDTO> Patients { get; set; }
        public ProfileDTO Profile { get; set; }
        public bool IsConfirmed { set; get; }
    }
}
