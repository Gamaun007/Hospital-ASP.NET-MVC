using Hospital.Areas.Patient.Models;
using Hospital.Areas.Profile.Models;
using Hospital.BusinessLogicLayer.DataTransferObjects;
using Hospital.BusinessLogicLayer.Enums;
using Hospital.BusinessLogicLayer.Interfaces;
using Hospital.Models.AutoMapper;
using Hospital.Util;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Areas.Patient.Controllers
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

        [HttpGet]
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
            if (!profileInfo.IsUserPatient && profile != null)
            {
                PatientRegistrationModel model = new PatientRegistrationModel();

                var number = UserService.GetUserPhoneNumber(User.Identity.GetUserId());
                if (number == null)
                {
                    return View(model);
                }
                ViewBag.PhoneNumberInfo = "Your PhoneNumber is already set, you can change if needed";
                model.PhoneNumber = number;
                return View(model);
            }
            return RedirectToAction("Index", "Page", new { area = "Patient" });
        }

        [HttpPost]
        [AuthorizeRoles(Roles.User)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientRegistrationModel model)
        {
            // var sp = HttpContext.Request.Form.Get("Specializations");
            if (ModelState.IsValid)
            {
                var patientDTO = MapperViewModel.PatientRegistrationModelToPatientDTO.Map<PatientRegistrationModel, PatientDTO>(model);
                try
                {
                    UserService.AddUserPatient(User.Identity.GetUserId(), patientDTO);
                }
                catch (Exception ex)
                {
                    ViewBag.PatientCreatingInfo = ex.Message;
                }
                return RedirectToAction("Index", "Information", new { area = "Profile" });
            }
            else
                return View(model);

        }

        [HttpPost]
        public ActionResult NewTreatment(int Id)
        {
            UserService.PatientNewTreatment(Id);
            return Index();
        }

        [HttpGet]
        [AuthorizeRoles(Roles.User)]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
           
            PatientDTO patDTO = null;
          //  ProfileDTO profDTO = null;
            try
            {
                patDTO = UserService.GetPatient(userId);
            //    profDTO = UserService.GetUserProfileInfo(User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
               return RedirectToAction("Create");
            }
            //var profileInfo = MapperViewModel.ProfileDTOToProfileViewModel.Map<ProfileDTO, ProfileViewModel>(profDTO);
            //if (!profileInfo.IsUserPatient)
            //{
            //    RedirectToAction("Create");
            //}
            var patViewModel = MapperViewModel.PatientDTOToPatientViewModel.Map<PatientDTO, PatientPageInfo>(patDTO);
            return View("Index",patViewModel);     
        }

    }

}
