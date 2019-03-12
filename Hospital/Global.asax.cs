using Hospital.App_Start;
using Hospital.BusinessLogicLayer.Interfaces;
using Hospital.BusinessLogicLayer.Util;
using Ninject;
using Ninject.Modules;
using Ninject.Mvc;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Hospital
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //  internal static StandardKernel Kernel { get; private set; }
        protected void Application_Start()
        {
            //NinjectContainer.RegisterAssembly();
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(ApplicationKernel.Kernel));

            
        }
    }
}
