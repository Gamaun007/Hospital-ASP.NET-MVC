using Hospital.BusinessLogicLayer.Interfaces;
using Hospital.BusinessLogicLayer.Util;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.App_Start
{
    public static class ApplicationKernel
    {
        private static StandardKernel _kernel;

        static ApplicationKernel()
        {
            NinjectModule UserServiceModule = new UserServiceRegistration();
            NinjectModule UnitOfRepositories = new UnitOfReposRegistration("DefaultConnection");
            _kernel = new StandardKernel(UserServiceModule, UnitOfRepositories);
        }

        public static StandardKernel Kernel
        {
            get
            {
                return _kernel;
            }
        }

        public static IUserService CreateUserService()
        {
            return Kernel.Get<IUserService>();
        }
    }

}