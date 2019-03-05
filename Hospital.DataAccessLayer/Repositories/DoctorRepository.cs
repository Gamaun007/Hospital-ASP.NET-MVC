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

        public Doctor Get(string userID)
        {
            //return DbContext.Doctors.Include(p => p.Profile)
            //    .Where(d => d.Profile.Id == user.Profile.Id).FirstOrDefault();        
            throw new NotImplementedException();
        }

        public void Create(Doctor doctor)
        {
            DbContext.Doctors.Add(doctor);
            DbContext.SaveChanges();
        }
    }
}
