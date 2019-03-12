using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Areas.Profile.Models
{
    public class ProfileViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        [Display(Name = "Main phone number")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public ICollection<string> Roles { get; set; }

        public bool IsUserPatient { get; set; }
        public bool IsPatientConfirmed { get; set; }

        public bool IsUserDoctor { get; set; }
        public bool IsDoctorConfirmed { get; set; }
    }
}