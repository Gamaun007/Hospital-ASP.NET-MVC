using Hospital.DataAccessLayer.Entities;
using Hospital.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccessLayer.Interfaces
{
    public interface IUnitOfRepositories : IDisposable
    {
        ApplicationRoleManager RoleManager { get; }
        ApplicationUserManager UserManager { get; }
        DoctorRepository DoctorRepository { get; }
        PatientRepository PatientRepository { get; }
        ProfileRepository ProfileRepository { get; }
        AdministrationRepository AdministrationRepository { get; }
        void SaveChanges();

    }
}
