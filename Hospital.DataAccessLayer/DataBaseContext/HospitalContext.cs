﻿using Hospital.DataAccessLayer.DataBaseContext.Seed;
using Hospital.DataAccessLayer.Entities;
using Hospital.DataAccessLayer.Entities.ExtraModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccessLayer.DataBaseContext
{
    public class HospitalContext : IdentityDbContext<ApplicationUser>
    {
       // Enable-Migrations -ProjectName Hospital.DataAccessLayer -StartUpProjectName Hospital -Verbose
        public HospitalContext(string conectionString) : base(conectionString) { }
        public HospitalContext() { }

        static HospitalContext()
        {
           Database.SetInitializer(new DropCreateInitializer());
        }

        public DbSet<UserProfile> Profiles { set; get; }
        public DbSet<Patient> Patients { set; get; }
        public DbSet<Doctor> Doctors { set; get; }
        public DbSet<MedicalCard> MedicalCard { set; get; }
        public DbSet<MedicalCardPage> CardPage { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Patient should have an MedicalCard, which owner is this Patient, 
            //Medical card should not be deleted from the DB
            modelBuilder.Entity<Patient>()
                .HasRequired(p => p.MedicalCard)
                .WithRequiredPrincipal(m => m.Patient)
                .WillCascadeOnDelete(false);
            
            // User profile CAN have an Patient account that has this profile
            modelBuilder.Entity<UserProfile>()
                .HasOptional(up => up.Patient)
                .WithRequired(p => p.Profile);

            // User profile CAN have an Doctor account that has this profile
            modelBuilder.Entity<UserProfile>()
               .HasOptional(up => up.Doctor)
               .WithRequired(p => p.Profile);

            // ApplicationUser SHOULD have an Profile, which owner is this ApplicationUser,
            //Profile should be cascade deleted from the DB 
            modelBuilder.Entity<ApplicationUser>()
                .HasRequired(au => au.Profile)
                .WithRequiredPrincipal(p => p.ApplicationUser)
                .WillCascadeOnDelete(true);

        }
    }
}
