using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Hospital.DataAccessLayer.Repositories;
using Hospital.DataAccessLayer.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Hospital.DataAccessLayer.DataBaseContext.Seed
{
    class CreateIfNotExists :  CreateDatabaseIfNotExists<HospitalContext>
    {
        protected override void Seed(HospitalContext context)
        {

            ApplicationUserManager UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            ProfileRepository profManager = new ProfileRepository(context);
            DoctorRepository doctorRep = new DoctorRepository(context);

            var adminRole = new ApplicationRole("Administrator");
            var doctorRole = new ApplicationRole("Doctor");
            var patientRole = new ApplicationRole("Patient");
            var userRole = new ApplicationRole("User");
            roleManager.Create(adminRole);
            roleManager.Create(doctorRole);
            roleManager.Create(patientRole);
            roleManager.Create(userRole);

            ApplicationUser user2 = new ApplicationUser { UserName = "usa@gmail.com",
                Email = "usa@gmail.com",
                Address = "White House",
                Name = "Donald Trump",
                PhoneNumber = "911911",
                Gender = "Male", BirthDate = new DateTime(1946,6,13) };

            var res1 = UserManager.Create(user2, "123456");
            if (res1.Succeeded)
            {
                UserManager.AddToRole(user2.Id, userRole.Name);
                UserManager.AddToRole(user2.Id, adminRole.Name);
            }

            UserProfile userProfile2 = new UserProfile { Id = user2.Id, ApplicationUser = user2 };
            profManager.Create(userProfile2);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
