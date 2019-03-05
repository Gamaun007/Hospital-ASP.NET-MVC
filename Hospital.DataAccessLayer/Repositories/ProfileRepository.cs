using Hospital.DataAccessLayer.DataBaseContext;
using Hospital.DataAccessLayer.Entities;
using Hospital.DataAccessLayer.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccessLayer.Repositories
{
    public class ProfileRepository : IRepository<UserProfile>
    {
        private HospitalContext DbContext { get; set; }
        public ProfileRepository(HospitalContext db)
        {
            DbContext = db;
        }

        //public void Create(ApplicationUser user)
        //{
        //    if (user.Profile == null)
        //    {
        //        DbContext.Profiles.Add(new UserProfile { ApplicationUser = user });
        //        DbContext.SaveChanges();
        //    }         
        //}

        public void Dispose()
        {
            DbContext.Dispose();
        }

        public UserProfile Get(string userId)
        {
            return DbContext.Profiles.Where(p => p.ApplicationUser.Id == userId).FirstOrDefault();
        }

        public void Create(UserProfile profile)
        {
            DbContext.Profiles.Add(profile);
            DbContext.SaveChanges();
        }
    }
}
