using AutoMapper;
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
        private static MapperConfiguration _profileDTOToProfileViewModel;

        static MapperViewModel()
        {
            _registerModelToUserDto = new MapperConfiguration(cfg => cfg.CreateMap<RegisterModel, UserDTO>()
                .ForMember("Name", opt => opt.MapFrom(c => String.Format("{0} {1}", c.FirstName, c.SecondName))));
  

            _loginModelToUserDto = new MapperConfiguration(cfg => cfg.CreateMap<LoginModel, UserDTO>());

            _profileDTOToProfileViewModel = new MapperConfiguration(cfg => cfg.CreateMap<ProfileDTO, ProfileViewModel>()
           .ForMember(d => d.IsUserPatient, opt => opt.Condition(src => (src.Patient == null) ? false : true))
           .ForMember(d => d.IsUserDoctor, opt => opt.Condition(src => (src.Doctor == null) ? false : true))
           .ForMember(d => d.Name, opt => opt.MapFrom(src => src.ApplicationUser.Name))
           .ForMember(d => d.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email))
           .ForMember(d => d.Address, opt => opt.MapFrom(src => src.ApplicationUser.Address))
           .ForMember(d => d.Roles, opt => opt.MapFrom(src => src.ApplicationUser.Roles)));
        }

        public static IMapper ProfileDTOToProfileViewModel
        {
            get
            {
               return _profileDTOToProfileViewModel.CreateMapper();
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