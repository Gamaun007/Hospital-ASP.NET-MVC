using AutoMapper;
using Hospital.DataAccessLayer.Entities;
using Hospital.DataAccessLayer.Entities.ExtraModels;
using Hospital.DataAccessLayer.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BusinessLogicLayer.DataTransferObjects.AutoMapper
{
    public class UserProfileToUserDTOProfile : Profile
    {
        public IUnitOfRepositories Repository { set; get; }

        public UserProfileToUserDTOProfile(IUnitOfRepositories rep)
        {
            Repository = rep;

            CreateMap<UserProfile, ProfileDTO>()
                   .ForMember(pdto => pdto.Doctor, opt => opt.MapFrom(s => s.Doctor))
                   .ForMember(pdto => pdto.Patient, opt => opt.MapFrom(s => s.Patient))
                   .ForMember(pdto => pdto.ApplicationUser, opt => opt.MapFrom(s => s.ApplicationUser));
                CreateMap<Patient, PatientDTO>();
                CreateMap<Doctor, DoctorDTO>();
                CreateMap<ApplicationUser, UserDTO>()
                .ForMember(d => d.Roles, opt => opt.MapFrom(src => ConvertRolesToStringCollection(src.Roles, Repository)));

           
        }


        public ICollection<string> ConvertRolesToStringCollection(ICollection<IdentityUserRole> ex, IUnitOfRepositories repo)
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
    }

    public class MedicalCardToCardDTO :Profile
    {
        public MedicalCardToCardDTO()
        {
            CreateMap<MedicalCard, MedicalCardDTO>()
                .ForMember(d => d.Pages, opt => opt.MapFrom(s => s.Pages));
            CreateMap<MedicalCardPage, MedicalCardPageDTO>();                 
        }
    }

}
