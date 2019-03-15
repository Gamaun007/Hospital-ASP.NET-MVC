using Hospital.Areas.Administration.Models;
using Hospital.Areas.Profile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Areas.Patient.Models
{
    public class PatientPageInfo
    {
        public int Id { set; get; }
        [Display(Name = "Phone number")]
        public string PhoneNumber { set; get; }
        [Display(Name = "Additional info.")]
        public string AdditionalInformation { set; get; }
        public bool IsDischarged { set; get; }
        public bool IsConfirmed { set; get; }
        public DoctorViewModel Doctor { set; get; }
        public ProfileViewModel Profile { get; set; }
    }
}