using AutoMapper;
using Hospital.Areas.Administration.Models;
using Hospital.Areas.Doctor.Models;
using Hospital.Areas.Patient.Models;
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
        private static MapperConfiguration _patientRegistrationModelToPatientDTO;
        private static MapperConfiguration _pageDTOToPageViewModel;
        private static MapperConfiguration _doctorDTOToDoctorViewModel;
        private static MapperConfiguration _doctorDTOToDoctorPageInfo;
        private static MapperConfiguration _patientDTOToPatientPageInfo;
        private static MapperConfiguration _patientDTOToPatientViewModel;
        private static MapperConfiguration _medicalCardDTOToPatientCardViewModel;
        private static MapperConfiguration _medCardPageViewModelToMedicalCardPageDTO;
        private static MapperConfiguration _profileDTOToProfileViewModel;

        static MapperViewModel()
        {
            _registerModelToUserDto = new MapperConfiguration(cfg => cfg.CreateMap<RegisterModel, UserDTO>()
                .ForMember("Name", opt => opt.MapFrom(c => String.Format("{0} {1}", c.FirstName, c.SecondName))));


            //_patientDTOToPatientConfirmation = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<PatientDTO, PatientConfirmationViewModel>();
            //    cfg.CreateMap<ProfileDTO, ProfileViewModel>()
            //     .ForMember(d => d.Name, opt => opt.MapFrom(src => src.ApplicationUser.Name))
            //    .ForMember(d => d.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email))
            //    .ForMember(d => d.Roles, opt => opt.MapFrom(src => src.ApplicationUser.Roles))
            //    .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(src => src.ApplicationUser.PhoneNumber))
            //    .ForMember(d => d.BirthDate, opt => opt.MapFrom(src => src.ApplicationUser.BirthDate))
            //    .ForMember(d => d.Gender, opt => opt.MapFrom(src => src.ApplicationUser.Gender))
            //    .ForMember(d => d.Address, opt => opt.MapFrom(src => src.ApplicationUser.Address))
            //    .ForAllOtherMembers(d => d.Ignore());
            //});

            _loginModelToUserDto = new MapperConfiguration(cfg => cfg.CreateMap<LoginModel, UserDTO>());

            _doctorRegistrationModelToDoctorDTO = new MapperConfiguration(cfg => cfg.CreateMap<DoctorRegistrationModel, DoctorDTO>()
           .ForMember(d => d.IsConfirmed, opt => opt.MapFrom(s => false))
           .ForMember( d=> d.Specialization, opt => opt.MapFrom(s=> s.SelectedSpecialization)));

            _patientRegistrationModelToPatientDTO = new MapperConfiguration(cfg => cfg.CreateMap<PatientRegistrationModel, PatientDTO>()
            .ForMember(d => d.MedicalCard, opt => opt.MapFrom(s => new MedicalCardDTO())));

            _pageDTOToPageViewModel = new MapperConfiguration(cfg => cfg.CreateMap<MedicalCardPageDTO, MedicalCardPageViewModel>());

            _doctorDTOToDoctorViewModel = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DoctorDTOToDoctorViewModel());
                cfg.AddProfile(new ProfileDTOForConfirmationModel());
            });

            _doctorDTOToDoctorPageInfo = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DoctorDTO, DoctorPageInfo>()
                .ForMember(d => d.Patients, opt => opt.Ignore())
                .ForMember(d => d.SelectedPatientId, opt => opt.Ignore());
                cfg.AddProfile(new ProfileDTOForConfirmationModel());
                
            });

            _patientDTOToPatientPageInfo = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PatientDTO, PatientPageInfo>()
                .ForMember(d => d.Doctor, opt => opt.MapFrom(s => s.Doctor));
                cfg.AddProfile(new DoctorDTOToDoctorViewModel());
                cfg.AddProfile(new ProfileDTOForConfirmationModel());

            });

            _patientDTOToPatientViewModel = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PatientDTO, PatientViewModel>();
                cfg.AddProfile(new ProfileDTOForConfirmationModel());
            });

            _medicalCardDTOToPatientCardViewModel = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MedicalCardDTO, PatientCardViewModel>()
                .ForMember(d => d.PatientName, opt => opt.MapFrom(s => s.Patient.Profile.ApplicationUser.Name))
                .ForMember( d => d.Pages , opt => opt.Ignore())
                .ForMember( d => d.SelectedPageId, opt => opt.Ignore());
            });

            _medCardPageViewModelToMedicalCardPageDTO = new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<MedicalCardPageViewModel, MedicalCardPageDTO>()
               .ForMember(d => d.MedicalCard, opt => opt.Ignore())
               .ForMember(d => d.NotationTime, opt  => opt.MapFrom(s => DateTime.Now));
           });

            _profileDTOToProfileViewModel = new MapperConfiguration(cfg => cfg.CreateMap<ProfileDTO, ProfileViewModel>()
           .ForMember(d => d.IsUserPatient, opt => opt.MapFrom(src => src.Patient == null ? false : true))
           .ForMember(d => d.IsUserDoctor, opt => opt.MapFrom(src => src.Doctor == null ? false : true))
           .ForMember(d => d.IsDoctorConfirmed, opt => opt.MapFrom(src => src.Doctor.IsConfirmed))
           .ForMember(d => d.IsPatientConfirmed, opt => opt.MapFrom(src => src.Patient.IsConfirmed))
           .ForMember(d => d.Name, opt => opt.MapFrom(src => src.ApplicationUser.Name))
           .ForMember(d => d.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email))
           .ForMember(d => d.Address, opt => opt.MapFrom(src => src.ApplicationUser.Address))
           .ForMember(d => d.Roles, opt => opt.MapFrom(src => src.ApplicationUser.Roles))
           .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(src => src.ApplicationUser.PhoneNumber))
           .ForMember(d => d.BirthDate, opt => opt.MapFrom ( src=> src.ApplicationUser.BirthDate))
           .ForMember(d => d.Gender, opt => opt.MapFrom (src => src.ApplicationUser.Gender)));
        }


        public static IMapper MedCardPageViewModelToMedicalCardPageDTO
        {
            get
            {
                return _medCardPageViewModelToMedicalCardPageDTO.CreateMapper();
            }
        }
        public static IMapper MedicalCardDTOToPatientCardViewModel
        {
            get
            {
                return _medicalCardDTOToPatientCardViewModel.CreateMapper();
            }
        }

        public static IMapper PatientDTOToPatientViewModel
        {
            get
            {
                return _patientDTOToPatientViewModel.CreateMapper();
            }
        }
        public static IMapper DoctorDTOToDoctorViewModel
        {
            get
            {
                return _doctorDTOToDoctorViewModel.CreateMapper();
            }
        }

        public static IMapper PageDTOToPageViewModel
        {
            get
            {
                return _pageDTOToPageViewModel.CreateMapper();
            }
        }

        public static IMapper PatientRegistrationModelToPatientDTO
        {
            get
            {
                return _patientRegistrationModelToPatientDTO.CreateMapper();
            }
        }

        public static IMapper DoctorDTOToDoctorPageInfo
        {
            get
            {
                return _doctorDTOToDoctorPageInfo.CreateMapper();
            }
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