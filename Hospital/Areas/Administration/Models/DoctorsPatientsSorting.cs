using Hospital.BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Areas.Administration.Models
{
    public static class DoctorsPatientsSorting
    {
        public static void ByName ( ref ICollection<PatientDTO> patients)
        {
            if (OrderFlags.PatientOnNameOrder)
            {
                patients = patients.OrderByDescending(x => x.Profile.ApplicationUser.Name).ToList();
                OrderFlags.PatientOnNameOrder = false;
            }
            else
            {
                patients = patients.OrderBy(x => x.Profile.ApplicationUser.Name).ToList();
                OrderFlags.PatientOnNameOrder = true;
            }
        }

        public static void BySpecialization(ref ICollection<DoctorDTO> doctors, string specialization)
        {
            var res = doctors.Select(x => x).Where(x => x.Specialization == specialization).ToList();
            if (res.Count > 0)
                doctors = res;
            
        }

        public static void ByName(ref ICollection<DoctorDTO> doctors)
        {
            if (OrderFlags.DoctorOnNameOrder)
            {
                doctors = doctors.OrderByDescending(x => x.Profile.ApplicationUser.Name).ToList();
                OrderFlags.DoctorOnNameOrder = false;
            }
            else
            {
                doctors = doctors.OrderBy(x => x.Profile.ApplicationUser.Name).ToList();
                OrderFlags.DoctorOnNameOrder = true;
            }
        }
        public static void ByBirthDate(ref ICollection<PatientDTO> patients)
        {
            if (OrderFlags.PatientOnBirthOrder)
            {
                patients = patients.OrderByDescending(x => x.Profile.ApplicationUser.BirthDate).ToList();
                OrderFlags.PatientOnBirthOrder = false;
            }
            else
            {
                patients = patients.OrderBy(x => x.Profile.ApplicationUser.BirthDate).ToList();
                OrderFlags.PatientOnBirthOrder = true;
            }
        }

        public static void BySpecialization(ref ICollection<DoctorDTO> doctors)
        {
            if (OrderFlags.DoctorOnSpecializationOrder)
            {
                doctors = doctors.OrderByDescending(x => x.Specialization).ToList();
                OrderFlags.DoctorOnSpecializationOrder = false;
            }
            else
            {
                doctors = doctors.OrderBy(x => x.Specialization).ToList();
                OrderFlags.DoctorOnSpecializationOrder = true;
            }
        }

        public static void ByPatientsCount(ref ICollection<DoctorDTO> doctors)
        {
            if (OrderFlags.DoctorOnPatientsOrder)
            {
                doctors = doctors.OrderByDescending(x => x.Patients.Count()).ToList();
                OrderFlags.DoctorOnPatientsOrder = false;
            }
            else
            {
                doctors = doctors.OrderBy(x => x.Patients.Count()).ToList();
                OrderFlags.DoctorOnPatientsOrder = true;
            }
        }
    }
}