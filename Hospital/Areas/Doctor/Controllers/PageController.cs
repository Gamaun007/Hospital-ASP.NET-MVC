using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.Areas.Administration.Models;
using Hospital.Areas.Doctor.Models;
using Hospital.Areas.Profile.Models;
using Hospital.BusinessLogicLayer.DataTransferObjects;
using Hospital.BusinessLogicLayer.Enums;
using Hospital.BusinessLogicLayer.Interfaces;
using Hospital.Models.AutoMapper;
using Hospital.Util;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Hospital.Areas.Doctor.Controllers
{
    public class PageController : Controller
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
        // GET: Doctor/Page
        [HttpGet]
        [AuthorizeRoles(Roles.User)]
        public ActionResult Create()
        {
            ProfileDTO profile = null;
            try
            {
                profile = UserService.GetUserProfileInfo(User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("Index", "Home", new { area = AreaReference.UseRoot });
            }
            var profileInfo = MapperViewModel.ProfileDTOToProfileViewModel.Map<ProfileDTO, ProfileViewModel>(profile);
            if (!profileInfo.IsUserDoctor && profile != null)
            {
                DoctorRegistrationModel model = new DoctorRegistrationModel();
                FillTheSpecializationList(model);

                var number = UserService.GetUserPhoneNumber(User.Identity.GetUserId());
                if (number == null)
                {
                    return View(model);
                }
                ViewBag.PhoneNumberInfo = "Your PhoneNumber is already set, you can change if needed";
                model.PhoneNumber = number;
                return View(model);
            }
            return RedirectToAction("Index", "Information", new { area = "Profile" });
        }

       

        [HttpPost]
        [AuthorizeRoles(Roles.User)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorRegistrationModel model)
        {
            // var sp = HttpContext.Request.Form.Get("Specializations");
            if (ModelState.IsValid)
            {
                var doctorDTO = MapperViewModel.DoctorRegistrationModelToDoctorDTO.Map<DoctorRegistrationModel, DoctorDTO>(model);
                try
                {
                    UserService.AddUserDoctor(User.Identity.GetUserId(), doctorDTO);
                }
                catch (Exception ex)
                {
                    ViewBag.DoctorCreatingInfo = ex.Message;
                }
                return RedirectToAction("Index", "Information", new { area = "Profile" });
            }
            else
            {
                FillTheSpecializationList(model);
                return View(model);
            }


        }

        private void FillTheSpecializationList(DoctorRegistrationModel model)
        {
            var specializationList = UserService.GetDoctorSpecializations;
            model.Specializations = specializationList.Select(x => new SelectListItem
            {
                Value = x,
                Text = x,
            });
        }

        private static ICollection<PatientDTO> DoctorPatients;

        [HttpGet]
        [AuthorizeRoles(Roles.Doctor)]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            DoctorDTO docDTO = null;
            try
            {
                docDTO = UserService.GetDoctor(userId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("Index", "Home", new { area = AreaReference.UseRoot });
            }
            var docPageModel = MapperViewModel.DoctorDTOToDoctorPageInfo.Map<DoctorDTO, DoctorPageInfo>(docDTO);
            DoctorPatients = docDTO.Patients;
            FillPatientsList(docPageModel, DoctorPatients);
            return View(docPageModel);
        }

        [HttpPost]
        public ActionResult GetPatientInfo(PatientViewModel pat)
        {
            return View();
        }

        private void FillPatientsList(DoctorPageInfo model, ICollection<PatientDTO> patients)
        {
            
            model.Patients = patients.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Profile.ApplicationUser.Name

            });
        }

        [HttpPost]   
        public PartialViewResult ShowPatientInfo(int? patientId)
        {

            var patientDTO = DoctorPatients.Select(x => x).Where(x => x.Id == patientId).FirstOrDefault();
            var res = MapperViewModel.PatientDTOToPatientViewModel.Map<PatientDTO, PatientViewModel>(patientDTO);
            return PartialView("GetPatientInfo", res);
        }

        // getting patinet id in parameters for showing his medical card
      
    }
}