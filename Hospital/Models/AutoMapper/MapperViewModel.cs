using AutoMapper;
using Hospital.Areas.Doctor.Models;
using Hospital.Areas.Profile.Models;
using Hospital.BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.AutoMapper
{
    public class MapperViewModel
    {
        private static MapperConfiguration _registerModelToUserDto;
        private static MapperConfiguration _loginModelToUserDto;
        private static MapperConfiguration _doctorRegistrationModelToDoctorDTO;
        private static MapperConfiguration _profileDTOToProfileViewModel;

        static MapperViewModel()
        {
            _registerModelToUserDto = new MapperConfiguration(cfg => cfg.CreateMap<RegisterModel, UserDTO>()
                .ForMember("Name", opt => opt.MapFrom(c => String.Format("{0} {1}", c.FirstName, c.SecondName))));
  

            _loginModelToUserDto = new MapperConfiguration(cfg => cfg.CreateMap<LoginModel, UserDTO>());

            _doctorRegistrationModelToDoctorDTO = new MapperConfiguration(cfg => cfg.CreateMap<DoctorRegistrationModel, DoctorDTO>()
           .ForMember(d => d.IsConfirmed, opt => opt.MapFrom(s => false))
           .ForMember( d=> d.Specialization, opt => opt.MapFrom(s=> s.SelectedSpecialization)));



            _profileDTOToProfileViewModel = new MapperConfiguration(cfg => cfg.CreateMap<ProfileDTO, ProfileViewModel>()
           .ForMember(d => d.IsUserPatient, opt => opt.MapFrom(src => IsPatientCondition(src.Patient)))
           .ForMember(d => d.IsUserDoctor, opt => opt.MapFrom(src => src.Doctor == null ? false : true))
           .ForMember(d => d.IsDoctorConfirmed, opt => opt.MapFrom(src => src.Doctor.IsConfirmed))
           .ForMember(d => d.IsPatientConfirmed, opt => opt.MapFrom(src => src.Patient.IsConfirmed))
           .ForMember(d => d.Name, opt => opt.MapFrom(src => src.ApplicationUser.Name))
           .ForMember(d => d.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email))
           .ForMember(d => d.Address, opt => opt.MapFrom(src => src.ApplicationUser.Address))
           .ForMember(d => d.Roles, opt => opt.MapFrom(src => src.ApplicationUser.Roles))
           .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(src => src.ApplicationUser.PhoneNumber)));
        }
        private static bool IsDoctorCondition(DoctorDTO model)
        {
            if (model == null)
                return false;
            else
                return true;
           
        }
        private static bool IsPatientCondition(PatientDTO model)
        {
            return model == null ? false : true;
        }
        public static IMapper ProfileDTOToProfileViewModel
        {
            get
            {
               return _profileDTOToProfileViewModel.CreateMapper();
            }
        }

        public static IMapper DoctorRegistrationModelToDoctorDTO
        {
            get
            {
                return _doctorRegistrationModelToDoctorDTO.CreateMapper();
            }
        }
        public static IMapper RegisterModelToUserDTO
        {
            get
            {
                return _registerModelToUserDto.CreateMapper();
            }
        }
        public static IMapper LoginModelToUserDTO
        {
            get
            {
                return _loginModelToUserDto.CreateMapper();
            }
        }

    }
}