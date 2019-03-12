using Hospital.DataAccessLayer.DataBaseContext;
using Hospital.DataAccessLayer.Entities;
using Hospital.DataAccessLayer.Interfaces;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.DataAccessLayer.Entities.ExtraModels;

namespace Hospital.DataAccessLayer.Repositories
{
    public class PatientRepository : IRepository<Patient>
    {
        private HospitalContext DbContext { get; set; }
        public PatientRepository(HospitalContext db)
        {
            DbContext = db;
        }

        //public void Create(ApplicationUser user, Patient patient)
        //{
        //    if (user != null && user.Profile != null && user.Profile.Patient == null)
        //    {
        //        DbContext.Patients.Add(new Patient)
        //    }
        //}

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public Patient Get(string userID)
        {
            return DbContext.Patients.Include(p => p.Profile).
                Where(p => p.Profile.ApplicationUser.Id == userID).FirstOrDefault();
        }

        public void Confirm(Patient pat)
        {
            pat.IsConfirmed = true;
        }

        public void AddPatientMedCardNotation ( int patientId, MedicalCardPage page)
        {
            var patient = DbContext.Patients.Where(p => p.Id == patientId).FirstOrDefault();
            var pageRes = DbContext.MedicalCardPages.Add(page);
            patient.MedicalCard.Pages.Add(page);
        }

        public Patient GetById(int patientID)
        {
            var res = DbContext.Patients.Include(p => p.MedicalCard.Pages).Where(d => d.Id == patientID).FirstOrDefault();
            return res;
            
        }

        public List<Patient> GetNotConfirmed()
        {
            return DbContext.Patients.Where(d => d.IsConfirmed == false).ToList();
        }

        //public void AddPatientDoctor(int patientId, Doctor doctor)
        //{
        //    DbContext.
        //}

        public List<Patient> GetConfirmed()
        {
            return DbContext.Patients.Where(d => d.IsConfirmed == true).ToList();
        }

        public void Create(Patient user)
        {
            DbContext.Patients.Add(user);
        }
    }
}
