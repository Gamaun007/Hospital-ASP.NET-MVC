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
    public class ControlPanelController : Controller
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


        // GET: Administration/ControlPanel
        [HttpGet]
        [AuthorizeRoles(Roles.Administrator)]
        public ActionResult Index()
        {

           var APanelDTO = UserService.GetAdminPanelInfo();
            var result = MapperViewModel.AdminPanelDTOTOControlPanelViewModel.Map<AdminPanelDTO, ControlPanelViewModel>(APanelDTO);
            return View(result);
        }
    }
}