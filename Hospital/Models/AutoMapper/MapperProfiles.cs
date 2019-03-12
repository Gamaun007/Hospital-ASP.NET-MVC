using AutoMapper;
using Hospital.Areas.Administration.Models;
using Hospital.Areas.Profile.Models;
using Hospital.BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models.AutoMapper
{
    public class ProfileDTOForConfirmationModel :Profile
    {
        public ProfileDTOForConfirmationModel()
        {
            CreateMap<ProfileDTO, ProfileViewModel>()
               .ForMember(d => d.Name, opt => opt.MapFrom(src => src.ApplicationUser.Name))
               .ForMember(d => d.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email))
               .ForMember(d => d.Roles, opt => opt.MapFrom(src => src.ApplicationUser.Roles))
               .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(src => src.ApplicationUser.PhoneNumber))
               .ForMember(d => d.BirthDate, opt => opt.MapFrom(src => src.ApplicationUser.BirthDate))
               .ForMember(d => d.Gender, opt => opt.MapFrom(src => src.ApplicationUser.Gender))
               .ForMember(d => d.Address, opt => opt.MapFrom(src => src.ApplicationUser.Address))
               .ForAllOtherMembers(d => d.Ignore());
        }
       
    }

    public class DoctorDTOToDoctorViewModel : Profile
    {
        public DoctorDTOToDoctorViewModel()
        {
            CreateMap<DoctorDTO, DoctorViewModel>()
                .ForMember(d => d.Patients, opt => opt.MapFrom(s => s.Patients.Count()));
                 
        }
    }
}