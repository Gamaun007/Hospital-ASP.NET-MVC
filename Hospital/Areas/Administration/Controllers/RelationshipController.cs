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
            RelationModel = new RelationshipModel();
            FillModel(RelationModel);
            return View(RelationModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Associate(RelationshipModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserService.Associate(model.SelectedDoctorId, model.SelectedPatientId);
                }
                catch (Exception e)
                {
                    return AssociateSamePersonError(model, e.Message);
                }
            }
            else
            {
                return AssociateModelError(model);
            }
            return RedirectToAction("Index");
        }

        private ViewResult AssociateSamePersonError(RelationshipModel model, string message)
        {
            ModelState.AddModelError("", message);
            FillModel(model);
            return View("Index", model);
        }

        private ViewResult AssociateModelError(RelationshipModel model)
        {
            if (model.SelectedDoctorId == null)
            {
                ModelState.AddModelError("", "Select Doctor to associate with");

            }
            if (model.SelectedPatientId == null)
            {
                ModelState.AddModelError("", "Select Patient to associate with");
            }
            FillModel(model);
            return View("Index", model);

        }
        public void FillModel(RelationshipModel model)
        {
            FillDoctorsList(model);
            FillPatientsList(model);
            FillSpecializationList(model);
        }

        //Model Fill methods

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
            PatientsConfirmed = UserService.GetPatientsWaitsForTreat().Where(p => p.Doctor == null).ToList();
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

        //Patients Ordering

        [HttpPost]
        public ViewResult OrderPatientByName()
        {
            DoctorsPatientsSorting.ByName(ref PatientsConfirmed);
            CreateSelectList(RelationModel, PatientsConfirmed);
            return View("Index", RelationModel);
        }

        [HttpPost]
        public ViewResult OrderPatientByBirthDate()
        {
            DoctorsPatientsSorting.ByBirthDate(ref PatientsConfirmed);
            CreateSelectList(RelationModel, PatientsConfirmed);
            return View("Index", RelationModel);
        }

        //Doctors Ordering

        [HttpPost]
        public ViewResult OrderDoctorByName()
        {
            DoctorsPatientsSorting.ByName(ref DoctorsConfirmed);
            CreateSelectList(RelationModel, DoctorsConfirmed);
            return View("Index", RelationModel);
        }
        [HttpPost]
        public ViewResult OrderSpecializationDoctors()
        {
            DoctorsPatientsSorting.BySpecialization(ref DoctorsConfirmed);
            CreateSelectList(RelationModel, DoctorsConfirmed);
            return View("Index", RelationModel);
        }

        [HttpPost]

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
        public PartialViewResult SortBySpecializ(string specialization)
        {
            if (specialization != "")
            {
                FillDoctorsList(RelationModel);
                DoctorsPatientsSorting.BySpecialization(ref DoctorsConfirmed, specialization);
                CreateSelectList(RelationModel, DoctorsConfirmed);
                RelationModel.SelectedSpecialization = specialization;
            }


            return PartialView("DoctorPatientList", RelationModel);
        }

        [HttpPost]
        public PartialViewResult CancelDoctorSorting(string specialization)
        {

            FillDoctorsList(RelationModel);
            RelationModel.SelectedSpecialization = specialization;
            return PartialView("DoctorPatientList", RelationModel);
        }

    }
}