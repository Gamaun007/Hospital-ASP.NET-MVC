using Hospital.BusinessLogicLayer.DataTransferObjects;
using Hospital.BusinessLogicLayer.Enums;
using Hospital.BusinessLogicLayer.Interfaces;
using Hospital.BusinessLogicLayer.Services;
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
using Hospital.Areas.Profile.Models;

namespace Hospital.Areas.Profile.Controllers
{
    public class InformationController : Controller
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

        // GET: Profile/Profile
        [AuthorizeRoles(Roles.User)]
        public ActionResult Index()
        {
            
            var userId = User.Identity.GetUserId();
            var profileDTO = UserService.GetUserProfileInfo(userId);
            var profileVM = MapperViewModel.ProfileDTOToProfileViewModel.Map<ProfileDTO, ProfileViewModel>(profileDTO);


            return View();
        }
    }
}