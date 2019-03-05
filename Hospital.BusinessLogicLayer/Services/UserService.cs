using Hospital.BusinessLogicLayer.DataTransferObjects;
using Hospital.BusinessLogicLayer.Enums;
using Hospital.BusinessLogicLayer.Interfaces;
using Hospital.DataAccessLayer.Entities;
using Hospital.DataAccessLayer.Enums;
using Hospital.DataAccessLayer.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Hospital.BusinessLogicLayer.DataTransferObjects.AutoMapper;

namespace Hospital.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        public IUnitOfRepositories RepoUnit { get; set; }

        public UserService(IUnitOfRepositories uor)
        {
            RepoUnit = uor;
        }


        public ClaimsIdentity Authenticate(UserDTO user)
        {
            ClaimsIdentity claim = null;

            ApplicationUser userFromDb = RepoUnit.UserManager.Find(user.Email, user.Password);
            // Authorize userFromDb if exists and set him cookies
            if (userFromDb != null)
                claim = RepoUnit.UserManager.CreateIdentity(userFromDb,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            else if (userFromDb == null)
                throw new ArgumentException("Wrong Email or Password");

            return claim;
        }
        public ProfileDTO GetUserProfileInfo(string userId)
        {
            ProfileDTO result = null;
            UserProfile profileFromDB = RepoUnit.ProfileRepository.Get(userId);
            if (profileFromDB != null)
            {
                
                var doc = MapperDTO.DoctorToDoctorDTO.Map<Doctor, DoctorDTO>(profileFromDB.Doctor);
                var pat = MapperDTO.PatientToPatientDTO.Map<Patient, PatientDTO>(profileFromDB.Patient);
                var user = MapperDTO.ApplicationUserToUserDTO.Map<ApplicationUser, UserDTO>(profileFromDB.ApplicationUser);
                MapperDTO mapRepo = new MapperDTO(RepoUnit);
                result = mapRepo.UserProfileToProfileDTO.Map<UserProfile, ProfileDTO>(profileFromDB);
            }
            return result;
        }

        public void Create(UserDTO user)
        {
            ApplicationUser userFromDb = RepoUnit.UserManager.FindByEmail(user.Email);
            if (userFromDb == null)
            {
                userFromDb = MapperDTO.UserDTOToApplicationUser.Map<UserDTO, ApplicationUser>(user);
                // userFromDb = new ApplicationUser { Email = user.Email, UserName = user.UserName };
                var result = RepoUnit.UserManager.Create(userFromDb, user.Password);
                // if (result.Errors.Count() > 0)


                if (!RepoUnit.RoleManager.RoleExists(Roles.User.ToString()))
                {
                    RepoUnit.RoleManager.Create(new ApplicationRole(Roles.User.ToString()));
                }

                // Add user role
                var UserRole = RepoUnit.RoleManager.FindByName(Roles.User.ToString()).Name;
                RepoUnit.UserManager.AddToRole(userFromDb.Id, UserRole);

                // Creating a profile for User
                UserProfile userProfile = new UserProfile { Id = userFromDb.Id, ApplicationUser = userFromDb };
                RepoUnit.ProfileRepository.Create(userProfile);
                RepoUnit.SaveChanges();
            }
            else
                throw new ApplicationException(String.Format("User with email {0} is already  registered in system!", userFromDb.Email));
        }

        public void Dispose()
        {
            RepoUnit.Dispose();
        }

        public void SetInitialRoles(List<Roles> roles)
        {
            foreach (var roleName in roles)
            {
                var role = RepoUnit.RoleManager.FindByName(roleName.ToString());
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName.ToString() };
                    RepoUnit.RoleManager.Create(role);
                }
            }
        }
    }
}
