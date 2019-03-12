using Hospital.Areas.Profile.Models;
using Hospital.BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Areas.Administration.Models
{
    public class DoctorViewModel
    {
        public int Id { set; get; }
        [Display(Name ="Phone number")]
        public string PhoneNumber { set; get; }
        public int Patients { set; get; }
        public string Specialization { get; set; }
        [Display(Name = "Additional info.")]
        public string AdditionalInformation { set; get; }
        public ProfileViewModel Profile { get; set; }
    }
}