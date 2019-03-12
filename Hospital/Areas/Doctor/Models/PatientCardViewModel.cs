using Hospital.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Areas.Doctor.Models
{
    public class PatientCardViewModel
    {
        [Display(Name = "Card Number")]
        public int Id { set; get; }
        [Display(Name = "Creating Date")]
        public DateTime CreatingDate { set; get; }

        [Display(Name = " Patient name")]
        public string PatientName { set; get; }

        [Required]
        public string SelectedPageId { get; set; }
        public IEnumerable<SelectListItem> Pages { get; set; }
    }
}