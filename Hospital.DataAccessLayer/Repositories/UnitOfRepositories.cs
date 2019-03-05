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
    public class UnitOfRepositories : IUnitOfRepositories
    {
        private HospitalContext _context;
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;
        private ProfileRepository _profileRepository;
        private DoctorRepository _doctorRepository;
        private PatientRepository _patientRepository;

        public UnitOfRepositories(string connectionString)
        {
            _context = new HospitalContext(connectionString);
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
            _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(_context));
            _profileRepository = new ProfileRepository(_context);
            _doctorRepository = new DoctorRepository(_context);
            _patientRepository = new PatientRepository(_context);
        }

        private bool disposed = false;

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager; }
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager; }
        }

        public DoctorRepository DoctorRepository
        {
            get { return _doctorRepository; }
        }

        public PatientRepository PatientRepository
        {
            get { return _patientRepository; }
        }

        public ProfileRepository ProfileRepository
        {
            get { return _profileRepository; }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _userManager.Dispose();
                    _roleManager.Dispose();
                    _profileRepository.Dispose();
                    _doctorRepository.Dispose();
                    _patientRepository.Dispose();

                }
                this.disposed = true;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
