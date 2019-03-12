using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Areas.Administration.Models
{
    public class ConfirmationModel
    {
        [Required]
        public string SelectedDoctorId { get; set; }
        //public string DoctorInfo { get; set; }
        [Required]
        public string SelectedPatientId { get; set; }

        public IEnumerable<SelectListItem> Doctors { get; set; }
        public IEnumerable<SelectListItem> Patients { get; set; }
    }
}