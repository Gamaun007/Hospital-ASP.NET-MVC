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
using Hospital.DataAccessLayer.Entities.ExtraModels;

namespace Hospital.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        public IUnitOfRepositories RepoUnit { get; set; }

        public ICollection<string> GetDoctorSpecializations
        {
            get
            {
                return  Enum.GetValues(typeof(Specialization)).Cast<Specialization>().Select(s => s.ToString()).ToList();
               
            }
        }

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
        public string GetUserPhoneNumber(string userId)
        {
           var number = RepoUnit.UserManager.GetPhoneNumber(userId);
           return number;
        }
        public ProfileDTO GetUserProfileInfo(string userId)
        {
            ProfileDTO result = null;
            UserProfile profileFromDB = RepoUnit.ProfileRepository.Get(userId);
            if (profileFromDB != null)
            {               
                //var doc = MapperDTO.DoctorToDoctorDTO.Map<Doctor, DoctorDTO>(profileFromDB.Doctor);
                //var pat = MapperDTO.PatientToPatientDTO.Map<Patient, PatientDTO>(profileFromDB.Patient);
                //var user = MapperDTO.ApplicationUserToUserDTO.Map<ApplicationUser, UserDTO>(profileFromDB.ApplicationUser);
                MapperDTO mapRepo = new MapperDTO(RepoUnit);
                return result = mapRepo.UserProfileToProfileDTO.Map<UserProfile, ProfileDTO>(profileFromDB);
            }
            else
            throw new ApplicationException("Profile not found");
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

        public void AddMedCardPage (int patientId, MedicalCardPageDTO page)
        {
            var mapper = new MapperDTO(RepoUnit);
            var res = mapper.MedCardPageDTOToMedCardPage.Map<MedicalCardPageDTO, MedicalCardPage>(page);
            RepoUnit.PatientRepository.AddPatientMedCardNotation(patientId, res);
            RepoUnit.SaveChanges();
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

        public void AddUserDoctor(string userId, DoctorDTO doctorDTO)
        {
           var profileFromDb = RepoUnit.ProfileRepository.Get(userId);
            if (profileFromDb != null && profileFromDb.Doctor == null)
            {
                var res = MapperDTO.DoctorDTOToDoctor.Map<DoctorDTO, Doctor>(doctorDTO);
                res.Profile = profileFromDb;
                RepoUnit.DoctorRepository.Create(res);

                RepoUnit.SaveChanges();
            }
            else throw new NullReferenceException("You can't create new doctor page, you are already a doctor, or waiting for confirmation");
        }

        public ICollection<DoctorDTO> GetDoctorsNotConfirmed()
        {
            ICollection<DoctorDTO> doctorDTOColl = new List<DoctorDTO>();
            var doctorlist = RepoUnit.DoctorRepository.GetNotConfirmed();
            MapperDTO mapRepo = new MapperDTO(RepoUnit);
            foreach (var doctor in doctorlist)
            {
                var transfer = mapRepo.DoctorToDoctorDTO.Map<Doctor, DoctorDTO>(doctor);
                doctorDTOColl.Add(transfer);
            }
            return doctorDTOColl;
        }
        public PatientDTO GetPatient(string userId)
        {
            Patient pat = RepoUnit.PatientRepository.Get(userId);     
            if (pat != null)
            {              
                var map = new MapperDTO(RepoUnit);
                var res = map.PatientToPatientDTO.Map<Patient, PatientDTO>(pat);
                return res;
            }
            else
                throw new ApplicationException("Patient not found");
        }

        public DoctorDTO GetDoctor(string userId)
        {
            Doctor doc = RepoUnit.DoctorRepository.Get(userId);
            if (doc != null)
            {
                var map = new MapperDTO(RepoUnit);
                var res = map.DoctorToDoctorDTO.Map<Doctor, DoctorDTO>(doc);
                return res;
            }
            else
                throw new ApplicationException("Doctor not found");
        }

        public ICollection<DoctorDTO> GetDoctorsConfirmed()
        {
            ICollection<DoctorDTO> doctorDTOColl = new List<DoctorDTO>();
            var doctorlist = RepoUnit.DoctorRepository.GetConfirmed();
            MapperDTO mapRepo = new MapperDTO(RepoUnit);
            foreach (var doctor in doctorlist)
            {
                var transfer = mapRepo.DoctorToDoctorDTO.Map<Doctor, DoctorDTO>(doctor);
                doctorDTOColl.Add(transfer);
            }
            return doctorDTOColl;
        }
        public void Associate (string doctorId, string patientId)
        {
            int parsedDoctorID;
            int parsedPatientID;
            int.TryParse(doctorId, out parsedDoctorID);
            int.TryParse(patientId, out parsedPatientID);
            var patient = RepoUnit.PatientRepository.GetById(parsedPatientID);
            RepoUnit.DoctorRepository.AddDoctorPatient(parsedDoctorID,patient);
            RepoUnit.SaveChanges();
        }

        public ICollection<PatientDTO> GetPatientsNotConfirmed()
        {
            ICollection<PatientDTO> patientDTOColl = new List<PatientDTO>();
            var patientList = RepoUnit.PatientRepository.GetNotConfirmed();
            MapperDTO mapRepo = new MapperDTO(RepoUnit);
            foreach (var patient in patientList)
            {
                var transfer = mapRepo.PatientToPatientDTO.Map<Patient, PatientDTO>(patient);
                patientDTOColl.Add(transfer);
            }
            return patientDTOColl;
        }

        public ICollection<PatientDTO> GetPatientsConfirmed()
        {
            ICollection<PatientDTO> patientDTOColl = new List<PatientDTO>();
            var patientList = RepoUnit.PatientRepository.GetConfirmed();
            MapperDTO mapRepo = new MapperDTO(RepoUnit);
            foreach (var patient in patientList)
            {
                var transfer = mapRepo.PatientToPatientDTO.Map<Patient, PatientDTO>(patient);
                patientDTOColl.Add(transfer);
            }
            return patientDTOColl;
        }

        public PatientDTO GetPatient(int patientId)
        {
            Patient pat = RepoUnit.PatientRepository.GetById(patientId);
            if (pat != null)
            {
                var map = new MapperDTO(RepoUnit);
                var res = map.PatientToPatientDTO.Map<Patient, PatientDTO>(pat);
                return res;
            }
            else
                throw new ApplicationException("Patient not found");
        }

        public void AddUserPatient(string userId, PatientDTO patientDTO)
        {
            var profileFromDb = RepoUnit.ProfileRepository.Get(userId);
            if (profileFromDb != null && profileFromDb.Patient == null)
            {
                var res = MapperDTO.PatientDTOToPatient.Map<PatientDTO, Patient>(patientDTO);
                res.Profile = profileFromDb;
                
                RepoUnit.PatientRepository.Create(res);

                //if (!RepoUnit.RoleManager.RoleExists(Roles.Patient.ToString()))
                //{
                //    var role = new ApplicationRole { Name = Roles.Patient.ToString() };
                //    RepoUnit.RoleManager.Create(role);
                //}

                //RepoUnit.UserManager.AddToRole(userId, Roles.Patient.ToString());
                RepoUnit.SaveChanges();
            }
            else throw new NullReferenceException("You can't create new doctor page, you are already a doctor, or waiting for confirmation");
        }

        public void RefuseDoctor(string Id)
        {
            int parsedID;
            int.TryParse(Id, out parsedID);

           var doctor = RepoUnit.DoctorRepository.GetById(parsedID);
            var docProfile = doctor.Profile;
            if(!doctor.IsConfirmed)
            {
                var userId = doctor.Profile.ApplicationUser.Id;
                //doctor= null;
                RepoUnit.ProfileRepository.RemoveFromDoctor(doctor);
                RepoUnit.UserManager.RemoveFromRole(userId, Roles.Doctor.ToString());
                RepoUnit.SaveChanges();
            }
          
        }

        public void ConfirmDoctor(string Id)
        {
            int parsedID;
            int.TryParse(Id, out parsedID);
            var doctor = RepoUnit.DoctorRepository.GetById(parsedID);
            var userId = doctor.Profile.ApplicationUser.Id;

            if (!doctor.IsConfirmed)
            {
                doctor.IsConfirmed = true;
                if (!RepoUnit.RoleManager.RoleExists(Roles.Doctor.ToString()))
                {
                    var role = new ApplicationRole { Name = Roles.Doctor.ToString() };
                    RepoUnit.RoleManager.Create(role);
                }

                RepoUnit.UserManager.AddToRole(userId, Roles.Doctor.ToString());
                RepoUnit.SaveChanges();
            }
        }

        public void RefusePatient(string Id)
        {
            int parsedID;
            int.TryParse(Id, out parsedID);

            var patient = RepoUnit.PatientRepository.GetById(parsedID);
            var patProfile = patient.Profile;
            if (!patient.IsConfirmed)
            {
                var userId = patient.Profile.ApplicationUser.Id;
                //doctor= null;
                RepoUnit.ProfileRepository.RemoveFromPatient(patient);
                RepoUnit.UserManager.RemoveFromRole(userId, Roles.Patient.ToString());
                RepoUnit.SaveChanges();
            }

        }

        public void ConfirmPatient(string Id)
        {
            int parsedID;
            int.TryParse(Id, out parsedID);
            var patient = RepoUnit.PatientRepository.GetById(parsedID);
            var userId = patient.Profile.ApplicationUser.Id;

            if (!patient.IsConfirmed)
            {
                RepoUnit.PatientRepository.Confirm(patient);
                if (!RepoUnit.RoleManager.RoleExists(Roles.Patient.ToString()))
                {
                    var role = new ApplicationRole { Name = Roles.Patient.ToString() };
                    RepoUnit.RoleManager.Create(role);
                }

                RepoUnit.UserManager.AddToRole(userId, Roles.Patient.ToString());
                RepoUnit.SaveChanges();
            }
            
        }
    }
}
