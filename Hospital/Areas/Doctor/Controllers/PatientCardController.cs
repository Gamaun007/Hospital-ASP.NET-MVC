using Hospital.Areas.Doctor.Models;
using Hospital.BusinessLogicLayer.DataTransferObjects;
using Hospital.BusinessLogicLayer.Interfaces;
using Hospital.Models.AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Hospital.Areas.Doctor.Controllers
{
    public class PatientCardController : Controller
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

        // GET: Doctor/PatientCard
        public ActionResult ShowInfo()
        {
            return View();
        }

        //[HttpGet]
        //private ActionResult Show()
        //{

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Show(int Id)
        {
            if (ModelState.IsValid)
            {
                PatientId = Id;
                var patientDTO = UserService.GetPatient(Id);
               
                var card = patientDTO.MedicalCard;
                Pages = card.Pages;
                var ViewModel = MapperViewModel.MedicalCardDTOToPatientCardViewModel.Map<MedicalCardDTO, PatientCardViewModel>(card);

                FillPagesList(ViewModel, Pages);

                return View("Show",ViewModel);
            }
            return RedirectToAction("Index", "Page");
        }

        private static int PatientId;
        private static ICollection<MedicalCardPageDTO> Pages;

        private void FillPagesList(PatientCardViewModel model, ICollection<MedicalCardPageDTO> pages)
        {

            model.Pages = pages.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.NotationTime.ToLongDateString()

            });
        }

       // private static MedicalCardPageViewModel CPViewModel;

        [HttpPost]
        public PartialViewResult ShowPageInfo(int? pageId)
        {
            var pageDTO = Pages.Select(x => x).Where(x => x.Id == pageId).FirstOrDefault();
            var res = MapperViewModel.PageDTOToPageViewModel.Map<MedicalCardPageDTO, MedicalCardPageViewModel>(pageDTO);
            return PartialView("GetCardPageInfo", res);
        }

        [HttpPost]
        public PartialViewResult CreateNotation(int Id)
        {
            var userid = User.Identity.GetUserId();
            var currentDoctor = UserService.GetDoctor(userid);
            if (currentDoctor.Specialization == "Nurse")
            {
                ViewBag.IsDoctorNurse = true;
            }
            else
                ViewBag.IsDoctorNurse = false;
            return PartialView("CreateNewCardPage");
        }

        [HttpPost]
        public ActionResult SubmitNotation(MedicalCardPageViewModel model)
        {
            var pageDTO = MapperViewModel.MedCardPageViewModelToMedicalCardPageDTO.Map<MedicalCardPageViewModel, MedicalCardPageDTO>(model);
            UserService.AddMedCardPage(PatientId, pageDTO);
            return Show(PatientId);
        }
    }
}