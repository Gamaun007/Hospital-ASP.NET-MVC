using Hospital.Areas.Doctor.Models;
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

        [HttpPost]
        [AuthorizeRoles(Roles.Patient)]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int Id)
        {
            if (ModelState.IsValid)
            {
                var PatientId = Id;
                var patientDTO = UserService.GetPatient(Id);

                var card = patientDTO.MedicalCard;
                Pages = card.Pages;
                var ViewModel = MapperViewModel.MedicalCardDTOToPatientCardViewModel.Map<MedicalCardDTO, PatientCardViewModel>(card);

                FillPagesList(ViewModel, Pages);

                return View(ViewModel);
            }
            return View();
//MAKE RETURN REDIRECT ACTION!!
        }

        private static ICollection<MedicalCardPageDTO> Pages;

        private void FillPagesList(PatientCardViewModel model, ICollection<MedicalCardPageDTO> pages)
        {

            model.Pages = pages.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.NotationTime.ToLongDateString()

            });
        }
        [HttpPost]
        public PartialViewResult ShowPageInfo(int? pageId)
        {
            var pageDTO = Pages.Select(x => x).Where(x => x.Id == pageId).FirstOrDefault();
            var res = MapperViewModel.PageDTOToPageViewModel.Map<MedicalCardPageDTO, MedicalCardPageViewModel>(pageDTO);
            return PartialView("GetCardPageInfo", res);
        }
    }
}