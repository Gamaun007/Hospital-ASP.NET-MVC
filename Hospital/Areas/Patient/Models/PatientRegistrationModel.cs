using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Areas.Patient.Models
{
    public class PatientRegistrationModel
    {
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { set; get; }

        [Required(ErrorMessage = "Please fill the")]
        [StringLength(300, MinimumLength = 30)]
        [Display(Name = "Information about your symptoms, etc.")]
        public string AdditionalInformation { set; get; }
    }
}