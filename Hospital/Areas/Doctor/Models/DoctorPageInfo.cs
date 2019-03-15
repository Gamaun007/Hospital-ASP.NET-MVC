using Hospital.Areas.Profile.Models;
using Hospital.BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Areas.Doctor.Models
{
    public class DoctorPageInfo
    {
        [Display(Name = "Phone number")]
        public string PhoneNumber { set; get; }

        public string Specialization { get; set; }
        public ProfileViewModel Profile { get; set; }
        public bool IsConfirmed { set; get; }

        [Required]
        public string SelectedPatientId { get; set; }
        public IEnumerable<SelectListItem> Patients { get; set; }
    }
}