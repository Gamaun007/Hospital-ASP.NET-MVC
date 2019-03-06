using Hospital.BusinessLogicLayer.DataTransferObjects;
using Hospital.BusinessLogicLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Hospital.BusinessLogicLayer.Interfaces
{
    public interface IUserService :IDisposable
    {
        void Create(UserDTO user);
        ClaimsIdentity Authenticate(UserDTO user);
        void SetInitialRoles(List<Roles> roles);
        ProfileDTO GetUserProfileInfo(string userId);
        string GetUserPhoneNumber(string userId);
        ICollection<string> GetDoctorSpecializations { get; }
        void AddUserDoctor(string userId,DoctorDTO doctorDTO);


    }
}
