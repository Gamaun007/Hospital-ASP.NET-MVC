using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Areas.Administration.Models
{
    public class ControlPanelViewModel
    {
        [Display(Name ="Patients in system")]
        public int PatientsInSystem { set; get; }
        [Display(Name = "Patients confirmed")]
        public int PatientsConfirmed { set; get; }
        [Display(Name = "Patients on treatment")]
        public int PatientsOnTreatment { set; get; }

        [Display(Name = "Doctors treat")]
        public int DoctorsTreat { set; get; }
        [Display(Name = "Doctors in system")]
        public int DoctorsInSystem { set; get; }
        [Display(Name = "Doctors confirmed")]
        public int DoctorsConfirmed { set; get; }

        [Display(Name = "Users in system")]
        public int Users { set; get; }
    }
}