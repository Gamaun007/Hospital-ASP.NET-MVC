using Hospital.DataAccessLayer.DataBaseContext;
using Hospital.DataAccessLayer.Entities;
using Hospital.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccessLayer.Repositories
{
    public class DoctorRepository : IRepository<Doctor>
    {
        private HospitalContext DbContext { get; set; }
        public DoctorRepository(HospitalContext db)
        {
            DbContext = db;
        }

        //public void Create(ApplicationUser user)
        //{
        //    if (user != null && user.Profile != null && user.Profile.Doctor == null)
        //    {
        //        DbContext.Doctors.Add(new Doctor { Profile = user.Profile });
        //        DbContext.SaveChanges();
        //    }
        //}
        //public void Create(UserProfile profile)
        //{
        //    if (profile !=null && profile.Doctor == null)
        //    {
        //        DbContext.Doctors.Add(new Doctor { Profile = profile });
        //        DbContext.SaveChanges();
        //    }
        //}


        public void Dispose()
        {
            DbContext.Dispose();
        }

        public Doctor GetById(int doctorId)
        {
            
            return DbContext.Doctors.Where(d => d.Id == doctorId).FirstOrDefault();
            
        }

        public void AddDoctorPatient(int doctorId, Patient patient)
        {
            var doc = DbContext.Doctors.Where(d => d.Id == doctorId).FirstOrDefault();
            doc.Patients.Add(patient);
        }

        public List<Doctor> GetNotConfirmed()
        {
            return DbContext.Doctors.Where(d => d.IsConfirmed == false).ToList();
        }

        public List<Doctor> GetConfirmed()
        {
            return DbContext.Doctors.Include(d => d.Patients).Where(d => d.IsConfirmed == true).ToList();
        }

        public void Create(Doctor doctor)
        {
            DbContext.Doctors.Add(doctor);
            //DbContext.SaveChanges();
        }



        public Doctor Get(string userId)
        {
            return DbContext.Doctors.Include(p => p.Patients ).Include(p => p.Profile).
               Where(p => p.Profile.ApplicationUser.Id == userId).FirstOrDefault();
        }

        public Doctor DischargePatient(int doctorId, int patientId)
        {
            var doc = DbContext.Doctors.Where(d => d.Id == doctorId).FirstOrDefault();
            var patient = DbContext.Patients.Where(p => p.Id == patientId && p.Doctor.Id == doctorId).FirstOrDefault();
            patient.IsDischarged = true;
            patient.Doctor = null;
            return doc;
        }
    }
}
