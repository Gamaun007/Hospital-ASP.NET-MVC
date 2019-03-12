using Hospital.BusinessLogicLayer.Enums;
using Hospital.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hospital.Areas.Administration.Controllers
{
    public class ControlPanelController : Controller
    {
        // GET: Administration/ControlPanel
        [HttpGet]
        [AuthorizeRoles(Roles.Administrator)]
        public ActionResult Index()
        {
            return View();
        }
    }
}