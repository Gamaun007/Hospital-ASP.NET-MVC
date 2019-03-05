using Hospital.DataAccessLayer.Entities;
using Hospital.DataAccessLayer.Enums;
using Hospital.DataAccessLayer.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccessLayer.DataBaseContext.Seed
{
    class DropCreateInitializer : DropCreateDatabaseAlways<HospitalContext>
    {
        protected override void Seed(HospitalContext context)
        {
            ApplicationUserManager UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            ProfileRepository profManager = new ProfileRepository(context);

            var adminRole = new ApplicationRole("Administrator");
            var doctorRole = new ApplicationRole("Doctor");
            var patientRole = new ApplicationRole("Patient");
            var userRole = new ApplicationRole("User");
            roleManager.Create(adminRole);
            roleManager.Create(doctorRole);
            roleManager.Create(patientRole);
            roleManager.Create(userRole);


            ApplicationUser user1 = new ApplicationUser { UserName = "Jimmy", Email = "somemail@gmail.com" };
            UserManager.Create(user1, "1234587");
            UserManager.AddToRole(user1.Id, userRole.Name);
            //profManager.CreateProfile(user1);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
