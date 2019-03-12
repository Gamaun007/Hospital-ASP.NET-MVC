using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Areas.Patient.Models
{
    public class MedicalCardViewModel
    {
        [Required]
        public string SelectedPageId { get; set; }
        public IEnumerable<SelectListItem> MedicalCardPages { get; set; }
    }
}