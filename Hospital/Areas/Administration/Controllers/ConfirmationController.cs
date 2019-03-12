using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.Areas.Administration.Models;
using Hospital.BusinessLogicLayer.DataTransferObjects;
using Hospital.BusinessLogicLayer.Enums;
using Hospital.BusinessLogicLayer.Interfaces;
using Hospital.Models.AutoMapper;
using Hospital.Util;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Hospital.Areas.Administration.Controllers
{
    public class ConfirmationController : Controller
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

        private static ICollection<DoctorDTO> DoctorsNotConfirmed;
        private static ICollection<PatientDTO> PatientsNotConfirmed;

        [HttpGet]
        [AuthorizeRoles(Roles.Administrator)]
        public ActionResult Index()
        {
            ConfirmationModel model = new ConfirmationModel();
            FillDoctorsList(model);
            FillPatientsList(model);

            return View(model);
        }

        private void FillDoctorsList(ConfirmationModel model)
        {
            DoctorsNotConfirmed = UserService.GetDoctorsNotConfirmed();
            model.Doctors = DoctorsNotConfirmed.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = String.Format("{0} <{1}> : {2} ",x.Profile.ApplicationUser.Name, x.Specialization, x.PhoneNumber),
                
            });
        }

        private void FillPatientsList(ConfirmationModel model)
        {
            PatientsNotConfirmed = UserService.GetPatientsNotConfirmed();
            model.Patients = PatientsNotConfirmed.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = String.Format("{0} : {1}", x.Profile.ApplicationUser.Name, x.PhoneNumber),

            });
        }

        public PartialViewResult ShowDoctorInfo(int doctorId)
        {

            var docDTO = DoctorsNotConfirmed.Select(x => x).Where(x => x.Id == doctorId).FirstOrDefault();
            var res = MapperViewModel.DoctorDTOToDoctorViewModel.Map<DoctorDTO,DoctorViewModel>(docDTO);
                return PartialView("GetDoctorInfo",res);
        }

        public PartialViewResult ShowPatientInfo(int patientId)
        {

            var patientDTO = PatientsNotConfirmed.Select(x => x).Where(x => x.Id == patientId).FirstOrDefault();
            var res = MapperViewModel.PatientDTOToPatientViewModel.Map<PatientDTO, PatientViewModel>(patientDTO);
            return PartialView("GetPatientInfo", res);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "ConfirmDoctor")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDoctor(ConfirmationModel model)
        {
            if (ModelState.IsValidField("SelectedDoctorId"))
            {
                UserService.ConfirmDoctor(model.SelectedDoctorId);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "You did not select the Doctor to Confirm!");
                
                FillDoctorsList(model);
                FillPatientsList(model);
                return View("Index", model);
            }
            
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "RefuseDoctor")]
        [ValidateAntiForgeryToken]
        public ActionResult RefuseDoctor(ConfirmationModel model)
        {
            if (ModelState.IsValidField("SelectedDoctorId"))
            {
                UserService.RefuseDoctor(model.SelectedDoctorId);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "You did not select the Doctor to Refuse!");
                
                FillDoctorsList(model);
                FillPatientsList(model);
                return View("Index", model);
            }
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "ConfirmPatient")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmPatient(ConfirmationModel model)
        {
            if (ModelState.IsValidField("SelectedPatientId"))
            {
                UserService.ConfirmPatient(model.SelectedPatientId);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "You did not select the Patient to Confirm!");
                FillPatientsList(model);
                FillDoctorsList(model);
                return View("Index", model);
            }

        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "RefusePatient")]
        [ValidateAntiForgeryToken]
        public ActionResult RefusePatient(ConfirmationModel model)
        {
            if (ModelState.IsValidField("SelectedPatientId"))
            {
                UserService.RefusePatient(model.SelectedPatientId);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "You did not select the Patient to Refuse!");
                FillPatientsList(model);
                FillDoctorsList(model);
                return View("Index", model);
            }
        }
    }
}