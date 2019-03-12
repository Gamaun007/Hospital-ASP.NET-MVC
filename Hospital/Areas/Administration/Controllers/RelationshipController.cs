using Hospital.Areas.Administration.Models;
using Hospital.BusinessLogicLayer.DataTransferObjects;
using Hospital.BusinessLogicLayer.Enums;
using Hospital.BusinessLogicLayer.Interfaces;
using Hospital.Models.AutoMapper;
using Hospital.Util;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Areas.Administration.Controllers
{
    public class RelationshipController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private static ICollection<DoctorDTO> DoctorsConfirmed;
        private static ICollection<PatientDTO> PatientsConfirmed;
        private static ICollection<string> Specializations;
        private static RelationshipModel RelationModel;
        private bool OnSpecSort { set; get; }
        // GET: Administration/Relationship
        [HttpGet]
        [AuthorizeRoles(Roles.Administrator)]
        public ActionResult Index()
        {
            //    if (OnSpecSort)
            //{
            //    return View(RelationModel);
            //}
                RelationModel = new RelationshipModel();
            //DoctorsConfirmed = UserService.GetDoctorsConfirmed();
            //Specializations = UserService.GetDoctorSpecializations;
            //PatientsConfirmed = UserService.GetPatientsConfirmed();

            //CreateSelectList(RelationModel, DoctorsConfirmed);
            //CreateSelectList(RelationModel, Specializations);
            //CreateSelectList(RelationModel, PatientsConfirmed);
            FillDoctorsList(RelationModel);
            FillPatientsList(RelationModel);
            FillSpecializationList(RelationModel);

            return View(RelationModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "Associate")]
        public ActionResult Associate(RelationshipModel model)
        {
            if (ModelState.IsValid)
            {
                UserService.Associate(model.SelectedDoctorId, model.SelectedPatientId);
            }
            return RedirectToAction("Index");
        }

        private void FillDoctorsList(RelationshipModel model)
        {
            DoctorsConfirmed = UserService.GetDoctorsConfirmed();
            CreateSelectList(model, DoctorsConfirmed);
        }

        private void FillSpecializationList(RelationshipModel model)
        {
            Specializations = UserService.GetDoctorSpecializations;
            CreateSelectList(model, Specializations);
        }

        private void FillPatientsList(RelationshipModel model)
        {
            PatientsConfirmed = UserService.GetPatientsConfirmed().Where( p => p.Doctor == null).ToList();
            CreateSelectList(model, PatientsConfirmed);
        }

        private void CreateSelectList(RelationshipModel model, ICollection<PatientDTO> patientDTO)
        {
            model.Patients = patientDTO.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = String.Format("{0} : {1}", x.Profile.ApplicationUser.Name, x.Profile.ApplicationUser.BirthDate.ToShortDateString()),
            });
        }
        private void CreateSelectList(RelationshipModel model, ICollection<DoctorDTO> doctorDTO)
        {
            model.Doctors = doctorDTO.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = String.Format("{0} <{1}> : Patients - {2} ", x.Profile.ApplicationUser.Name, x.Specialization, x.Patients.Count),

            });
        }
        private void CreateSelectList(RelationshipModel model, ICollection<string> specializations)
        {
            model.Specializations = Specializations.Select(x => new SelectListItem
            {
                Value = x.ToString(),
                Text = x.ToString()
            });
        }

        //Patients sorting
        [HttpPost]
       // [MultipleButton(Name = "action", Argument = "OrderNamePatients")]
        public ViewResult OrderPatientByName()
        {
            DoctorsPatientsSorting.ByName(ref PatientsConfirmed);
            CreateSelectList(RelationModel, PatientsConfirmed);
            return View("Index", RelationModel);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "OrderBirthPatients")]
        public ViewResult OrderPatientByBirthDate()
        {
            DoctorsPatientsSorting.ByBirthDate(ref PatientsConfirmed);
            CreateSelectList(RelationModel, PatientsConfirmed);
            return View("Index", RelationModel);
        }

        //Doctors Ordering

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "OrderNameDoctors")]
        public ViewResult OrderDoctorByName()
        {
            DoctorsPatientsSorting.ByName(ref DoctorsConfirmed);
            CreateSelectList(RelationModel, DoctorsConfirmed);
            return View("Index", RelationModel);
        }
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "OrderSpecializationDoctors")]
        public ViewResult OrderSpecializationDoctors()
        {
            DoctorsPatientsSorting.BySpecialization(ref DoctorsConfirmed);
            CreateSelectList(RelationModel, DoctorsConfirmed);
            return View("Index", RelationModel);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "OrderPatientsDoctors")]
        public ViewResult OrderPatientsDoctors()
        {
            DoctorsPatientsSorting.ByPatientsCount(ref DoctorsConfirmed);
            CreateSelectList(RelationModel, DoctorsConfirmed);
            return View("Index", RelationModel);
        }

        // Function that calls by Java sript from view

        public PartialViewResult ShowDoctorInfo(int doctorId)
        {
            var docDTO = DoctorsConfirmed.Select(x => x).Where(x => x.Id == doctorId).FirstOrDefault();
            var res = MapperViewModel.DoctorDTOToDoctorViewModel.Map<DoctorDTO, DoctorViewModel>(docDTO);
            return PartialView("GetDoctorInfo", res);
        }

        public PartialViewResult ShowPatientInfo(int patientId)
        {

            var patientDTO = PatientsConfirmed.Select(x => x).Where(x => x.Id == patientId).FirstOrDefault();
            var res = MapperViewModel.PatientDTOToPatientViewModel.Map<PatientDTO, PatientViewModel>(patientDTO);
            return PartialView("GetPatientInfo", res);
        }

        [HttpPost]
        public ViewResult SortBySpecializ(string specialization)
        {
            if (specialization != "")
            {
                FillDoctorsList(RelationModel);
                DoctorsPatientsSorting.BySpecialization(ref DoctorsConfirmed, specialization);
                CreateSelectList(RelationModel, DoctorsConfirmed);
                RelationModel.SelectedSpecialization = specialization;
            }
           
            
           return View("Index",RelationModel);
        }

        [HttpPost]
        public ViewResult CancelDoctorSorting ()
        {

            //FillDoctorsList(RelationModel);
            return View("Index", RelationModel);
        }

    }
}