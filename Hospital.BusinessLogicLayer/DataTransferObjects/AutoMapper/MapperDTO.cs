using AutoMapper;
using Hospital.DataAccessLayer.Entities;
using Hospital.DataAccessLayer.Entities.ExtraModels;
using Hospital.DataAccessLayer.Enums;
using Hospital.DataAccessLayer.Interfaces;
using Hospital.DataAccessLayer.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BusinessLogicLayer.DataTransferObjects.AutoMapper
{
    public class MapperDTO
    {
        private static MapperConfiguration _userDTOToApplicationUser;
        private static MapperConfiguration _DoctorDTOtoDoctor;
        private static MapperConfiguration _userProfileToProfileDTO;
        private static MapperConfiguration _applicationUserToUserDTO;
        private static MapperConfiguration _doctorToDoctorDTO;
        private static MapperConfiguration _patientToPatientDTO;
        private static MapperConfiguration _patientDTOToPatient;
        private MapperConfiguration _medCardPageDTOToMedCardPage;

        static MapperDTO()
        {
            _userDTOToApplicationUser = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, ApplicationUser>().
            ForMember(a => a.Id, opt => opt.UseDestinationValue())
            .ForMember(a => a.UserName, opt => opt.MapFrom(c => c.Email)));

            _DoctorDTOtoDoctor = new MapperConfiguration(cfg => cfg.CreateMap<DoctorDTO, Doctor>().
             ForMember(d => d.Specialization, opt => opt.MapFrom(s => (Specialization)Enum.Parse(typeof(Specialization), s.Specialization, true)))
             .ForMember(d => d.Id, opt => opt.UseDestinationValue()));

            _applicationUserToUserDTO = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, UserDTO>());

            _patientDTOToPatient = new MapperConfiguration(cfg => cfg.CreateMap<PatientDTO, Patient>()
            .ForMember(d => d.MedicalCard, opt => opt.MapFrom(s => new MedicalCard())));
        }

        public IUnitOfRepositories Repository { set; get; }

        

        public MapperDTO(IUnitOfRepositories rep)
        {
            Repository = rep;

            _medCardPageDTOToMedCardPage = new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<MedicalCardPageDTO, MedicalCardPage>()
               .ForMember(d => d.Id, opt => opt.UseDestinationValue());
           });

            _doctorToDoctorDTO = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Doctor, DoctorDTO>()
                    .ForMember(d => d.Patients, opt => opt.MapFrom(s => s.Patients))
                    .ForMember(d => d.Profile, opt => opt.MapFrom(s => s.Profile))
                    .ForMember(d => d.Specialization, opt => opt.MapFrom(s => s.Specialization.ToString()));
                cfg.AddProfile(new UserProfileToUserDTOProfile(rep));
                //cfg.CreateMap<UserProfile, ProfileDTO>()
                //  .ForMember(pdto => pdto.Doctor, opt => opt.MapFrom(s => s.Doctor))
                //  .ForMember(pdto => pdto.Patient, opt => opt.MapFrom(s => s.Patient))
                //  .ForMember(pdto => pdto.ApplicationUser, opt => opt.MapFrom(s => s.ApplicationUser));
                //cfg.CreateMap<Patient, PatientDTO>();
                //cfg.CreateMap<ApplicationUser, UserDTO>()
                // .ForMember(d => d.Roles, opt => opt.MapFrom(src => ConvertRolesToStringCollection(src.Roles, Repository)));
            });

            _patientToPatientDTO = new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<Patient, PatientDTO>();
               cfg.AddProfile(new UserProfileToUserDTOProfile(rep));
               cfg.AddProfile(new MedicalCardToCardDTO());
           });


            _userProfileToProfileDTO = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserProfileToUserDTOProfile(rep));
                //cfg.CreateMap<UserProfile, ProfileDTO>()
                //   .ForMember(pdto => pdto.Doctor, opt => opt.MapFrom(s => s.Doctor))
                //   .ForMember(pdto => pdto.Patient, opt => opt.MapFrom(s => s.Patient))
                //   .ForMember(pdto => pdto.ApplicationUser, opt => opt.MapFrom(s => s.ApplicationUser));
                //cfg.CreateMap<Patient, PatientDTO>();
                //cfg.CreateMap<Doctor, DoctorDTO>();
                //cfg.CreateMap<ApplicationUser, UserDTO>()
                //.ForMember(d => d.Roles, opt => opt.MapFrom(src => ConvertRolesToStringCollection(src.Roles, Repository)));
            });
        }

        private static ICollection<string> ConvertRolesToStringCollection(ICollection<IdentityUserRole> ex, IUnitOfRepositories repo)
        {
            var res = new List<string>();
            foreach (var role in ex)
            {
                var roleexist = repo.RoleManager.FindById(role.RoleId);
                if (roleexist != null)
                    res.Add(roleexist.Name);
            }
            return res;
        }

        public IMapper MedCardPageDTOToMedCardPage
        {
            get
            {
                return _medCardPageDTOToMedCardPage.CreateMapper();
            }
        }

        public IMapper PatientToPatientDTO
        {
            get
            {
                return _patientToPatientDTO.CreateMapper();
            }
        }
        public static IMapper PatientDTOToPatient
        {
            get
            {
                return _patientDTOToPatient.CreateMapper();
            }
        }

        public static IMapper ApplicationUserToUserDTO
        {
            get
            {
                return _applicationUserToUserDTO.CreateMapper();
            }
        }
        public static IMapper DoctorDTOToDoctor
        {
            get
            {
                return _doctorToDoctorDTO.CreateMapper();
            }
        }

        public IMapper UserProfileToProfileDTO
        {
            get
            {

                return _userProfileToProfileDTO.CreateMapper();
            }
        }

        public IMapper DoctorToDoctorDTO
        {
            get
            {

                return _doctorToDoctorDTO.CreateMapper();
            }
        }
        public static IMapper UserDTOToApplicationUser
        {
            get
            {
                return _userDTOToApplicationUser.CreateMapper();
            }
        }

        //public static IMapper DoctorToDoctorDTO
        //{
        //    get
        //    {
        //        return _doctorToDoctorDTO.CreateMapper();
        //    }
        //}

        //public static IMapper PatientToPatientDTO
        //{
        //    get
        //    {
        //        return _patientToPatientDTO.CreateMapper();
        //    }
        //}

    }
}
