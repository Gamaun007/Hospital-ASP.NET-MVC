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
    public class AdministrationRepository : IDisposable
    {
        private HospitalContext DbContext { get; set; }
        public AdministrationRepository(HospitalContext db)
        {
            DbContext = db;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }



        public int GetCount(Func<Patient,bool> expression)
        {
            return DbContext.Patients.Count(expression);
        }
        public int GetCount(Func<Doctor, bool> expression)
        {
            return DbContext.Doctors.Count(expression);
        }
        public int GetCount(Func<ApplicationUser, bool> expression)
        {
            return DbContext.Users.Count(expression);
        }


    }
}
