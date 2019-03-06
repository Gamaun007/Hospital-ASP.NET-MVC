using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Areas.Doctor.Models
{
    public class DoctorRegistrationModel
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { set; get; }
        
        public IEnumerable<SelectListItem> Specializations {set; get;}

        [Required(ErrorMessage = "Please fill the" )]
        [StringLength(300,MinimumLength =30)]
        [Display(Name = "Information about your skills, etc.")]
        public string AdditionalInformation { set; get; }
        [Required(ErrorMessage = "Please select the specialization from the list")]
        public string SelectedSpecialization { set; get; }
    }
}