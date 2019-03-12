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
            DoctorRepository doctorRep = new DoctorRepository(context);
            PatientRepository patientRep = new PatientRepository(context);

            var adminRole = new ApplicationRole("Administrator");
            var doctorRole = new ApplicationRole("Doctor");
            var patientRole = new ApplicationRole("Patient");
            var userRole = new ApplicationRole("User");
            roleManager.Create(adminRole);
            roleManager.Create(doctorRole);
            roleManager.Create(patientRole);
            roleManager.Create(userRole);

            ApplicationUser user2 = new ApplicationUser
            {
                UserName = "usa@gmail.com",
                Email = "usa@gmail.com",
                Address = "White House",
                Name = "Donald Trump",
                PhoneNumber = "911911",
                Gender = "Male",
                BirthDate = new DateTime(1946, 6, 13)
            };

            ApplicationUser user3 = new ApplicationUser
            {
                UserName = "baday@gmail.com",
                Email = "baday@gmail.com",
                Address = "Sportivnaya",
                Name = "Vasily Baday",
                PhoneNumber = "+38095553212",
                Gender = "Male",
                BirthDate = new DateTime(1993, 6, 10)
            };

            ApplicationUser user4 = new ApplicationUser
            {
                UserName = "panov@gmail.com",
                Email = "panov@gmail.com",
                Address = "K. Marksa 28B",
                Name = "Mihail Panov",
                PhoneNumber = "+380954343212",
                Gender = "Male",
                BirthDate = new DateTime(1989, 12, 29)
            };

            ApplicationUser user5 = new ApplicationUser
            {
                UserName = "movchan@gmail.com",
                Email = "movchan@gmail.com",
                Address = "Lermontova 28",
                Name = "Artem Movchan",
                PhoneNumber = "+3809543423",
                Gender = "Male",
                BirthDate = new DateTime(1992, 3, 7)
            };

            ApplicationUser user6 = new ApplicationUser
            {
                UserName = "babenko@gmail.com",
                Email = "babenko@gmail.com",
                Address = "Lermontova 17",
                Name = "Oleg Babenko",
                PhoneNumber = "+380955883423",
                Gender = "Male",
                BirthDate = new DateTime(1998, 10, 8)
            };

            ApplicationUser user7 = new ApplicationUser
            {
                UserName = "dudka@gmail.com",
                Email = "dudka@gmail.com",
                Address = "Zakori street 31",
                Name = "Oleg Dudka",
                PhoneNumber = "+3809323423",
                Gender = "Male",
                BirthDate = new DateTime(1996, 9, 8)
            };

            ApplicationUser user8 = new ApplicationUser
            {
                UserName = "lavrik@gmail.com",
                Email = "lavrik@gmail.com",
                Address = "Lenin street 189-B",
                Name = "Vitaly Lavrik",
                PhoneNumber = "+38092234565",
                Gender = "Male",
                BirthDate = new DateTime(1976, 4, 9)
            };

            ApplicationUser user9 = new ApplicationUser
            {
                UserName = "dub@gmail.com",
                Email = "dub@gmail.com",
                Address = "Lenin street 102-B",
                Name = "Natalya Dub",
                PhoneNumber = "+38092634565",
                Gender = "Female",
                BirthDate = new DateTime(2000, 6, 17)
            };

            ApplicationUser user10 = new ApplicationUser
            {
                UserName = "marchuk@gmail.com",
                Email = "marchuk@gmail.com",
                Address = "Ludviag Svobodi 51-B",
                Name = "Sergey Marchuk",
                PhoneNumber = "+3806646565",
                Gender = "Male",
                BirthDate = new DateTime(1998, 2, 8)
            };

            var res2 = UserManager.Create(user2, "123456");
            var res3 = UserManager.Create(user3, "123456");
            var res4 = UserManager.Create(user4, "123456");
            var res5 = UserManager.Create(user5, "123456");
            var res6 = UserManager.Create(user6, "123456");
            var res7 = UserManager.Create(user7, "123456");
            var res8 = UserManager.Create(user8, "123456");
            var res9 = UserManager.Create(user9, "123456");
            var res10 = UserManager.Create(user10, "123456");
            if (res2.Succeeded)
            {
                UserManager.AddToRole(user2.Id, userRole.Name);
                UserManager.AddToRole(user2.Id, adminRole.Name);

                UserManager.AddToRole(user3.Id, userRole.Name);
                UserManager.AddToRole(user4.Id, userRole.Name);
                UserManager.AddToRole(user5.Id, userRole.Name);
                UserManager.AddToRole(user6.Id, userRole.Name);
                UserManager.AddToRole(user7.Id, userRole.Name);
                UserManager.AddToRole(user8.Id, userRole.Name);
                UserManager.AddToRole(user9.Id, userRole.Name);
                UserManager.AddToRole(user10.Id, userRole.Name);

            }

            UserProfile userProfile2 = new UserProfile { Id = user2.Id, ApplicationUser = user2 };
            UserProfile userProfile3 = new UserProfile { Id = user3.Id, ApplicationUser = user3 };
            UserProfile userProfile4 = new UserProfile { Id = user4.Id, ApplicationUser = user4 };
            UserProfile userProfile5 = new UserProfile { Id = user5.Id, ApplicationUser = user5 };
            UserProfile userProfile6 = new UserProfile { Id = user6.Id, ApplicationUser = user6 };
            UserProfile userProfile7 = new UserProfile { Id = user7.Id, ApplicationUser = user7 };
            UserProfile userProfile8 = new UserProfile { Id = user8.Id, ApplicationUser = user8 };
            UserProfile userProfile9 = new UserProfile { Id = user9.Id, ApplicationUser = user9 };
            UserProfile userProfile10 = new UserProfile { Id = user10.Id, ApplicationUser = user10 };

            Patient patient1 = new Patient { AdditionalInformation = " some text some text some text some text ", IsConfirmed = false, Profile = userProfile3 };
            Patient patient2 = new Patient { AdditionalInformation = " some text some text some text some text ", IsConfirmed = false, Profile = userProfile4 };
            Patient patient3 = new Patient { AdditionalInformation = " some text some text some text some text ", IsConfirmed = false, Profile = userProfile5 };
            Patient patient4 = new Patient { AdditionalInformation = " some text some text some text some text ", IsConfirmed = false, Profile = userProfile6 };

            Doctor doctor1 = new Doctor { Specialization = Specialization.Dentist, IsConfirmed = false, AdditionalInformation = " some another doctor text for testing", Profile = userProfile7 };
            Doctor doctor2 = new Doctor { Specialization = Specialization.Nurse, IsConfirmed = false, AdditionalInformation = " some another doctor text for testing", Profile = userProfile8 };
            Doctor doctor3 = new Doctor { Specialization = Specialization.Pediatrician, IsConfirmed = false, AdditionalInformation = " some another doctor text for testing", Profile = userProfile9 };
            Doctor doctor4 = new Doctor { Specialization = Specialization.Dentist, IsConfirmed = false, AdditionalInformation = " some another doctor text for testing", Profile = userProfile10 };

            profManager.Create(userProfile2);
            profManager.Create(userProfile3);
            profManager.Create(userProfile4);
            profManager.Create(userProfile5);
            profManager.Create(userProfile6);
            profManager.Create(userProfile7);
            profManager.Create(userProfile8);
            profManager.Create(userProfile9);
            profManager.Create(userProfile10);

            doctorRep.Create(doctor1);
            doctorRep.Create(doctor2);
            doctorRep.Create(doctor3);
            doctorRep.Create(doctor4);

            patientRep.Create(patient1);
            patientRep.Create(patient2);
            patientRep.Create(patient3);
            patientRep.Create(patient4);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
