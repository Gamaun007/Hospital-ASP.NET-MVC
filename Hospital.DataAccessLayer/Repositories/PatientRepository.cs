using Hospital.DataAccessLayer.DataBaseContext;
using Hospital.DataAccessLayer.Entities;
using Hospital.DataAccessLayer.Interfaces;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Create(Patient user)
        {
            DbContext.Patients.Add(user);
            DbContext.SaveChanges();
        }
    }
}
