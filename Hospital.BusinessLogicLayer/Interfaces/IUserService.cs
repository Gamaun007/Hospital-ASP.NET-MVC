using Hospital.BusinessLogicLayer.DataTransferObjects;
using Hospital.BusinessLogicLayer.Enums;
using Hospital.DataAccessLayer.Entities;
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
        AdminPanelDTO GetAdminPanelInfo();
        ICollection<PatientDTO> GetPatientsWaitsForTreat();
        void PatientNewTreatment(int patientId);


        void AddUserDoctor(string userId,DoctorDTO doctorDTO);
        void AddUserPatient(string userId, PatientDTO patientDTO);

        ICollection<DoctorDTO> GetDoctorsNotConfirmed();
        ICollection<DoctorDTO> GetDoctorsConfirmed();
        DoctorDTO GetDoctor(string userId);

        ICollection<PatientDTO> GetPatientsNotConfirmed();
        ICollection<PatientDTO> GetPatientsConfirmed();
        PatientDTO GetPatient(string userId);
        PatientDTO GetPatient(int patientId);
        void AddMedCardPage(int patientId, MedicalCardPageDTO page);

        void DischargePatient(int patientId);

        void ConfirmDoctor(string Id);
        void ConfirmPatient(string Id);
        void RefuseDoctor(string Id);
        void RefusePatient(string Id);

        void Associate(string doctorId, string patientId);


    }
}
