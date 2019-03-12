using Hospital.Areas.Patient.Models;
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
    

    public class MedicalCardController : Controller
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
        [AuthorizeRoles(Roles.Patient)]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            PatientDTO patDTO = null;
            try
            {
                patDTO = UserService.GetPatient(userId);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return RedirectToAction("Index", "Home", new { area = AreaReference.UseRoot });
            }
            var patViewModel = MapperViewModel.PatientDTOToPatientViewModel.Map<PatientDTO, PatientPageInfo>(patDTO);
            return View(patViewModel);
        }

        private void FillDoctorsList(MedicalCardViewModel model, PatientDTO patient)
        {
            var mcard = patient.MedicalCard;
            model.MedicalCardPages = mcard.Pages.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.NotationTime.ToLongDateString()

            });
        }
    }
}