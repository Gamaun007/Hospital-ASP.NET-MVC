using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Areas.Administration.Models
{
    public static class OrderFlags
    {
        public static bool DoctorOnNameOrder { get; set; }
        public static bool DoctorOnSpecializationOrder { get; set; }
        public static bool DoctorOnSpecializationSort { get; set; }
        public static bool DoctorOnPatientsOrder { get; set; }

        public static bool PatientOnNameOrder { get; set;}
        public static bool PatientOnBirthOrder { get; set; }
    }
}