using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hospital.Areas.Doctor.Models;
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

        [HttpPost]
        [AuthorizeRoles(Roles.User)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorRegistrationModel model )
         {
           // var sp = HttpContext.Request.Form.Get("Specializations");
            if(ModelState.IsValid)
            {
                var doctorDTO = MapperViewModel.DoctorRegistrationModelToDoctorDTO.Map<DoctorRegistrationModel, DoctorDTO>(model);
                UserService.AddUserDoctor(User.Identity.GetUserId(), doctorDTO);
                return RedirectToAction("Index","Information",new { area = "Profile" }) ;
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
    }
}