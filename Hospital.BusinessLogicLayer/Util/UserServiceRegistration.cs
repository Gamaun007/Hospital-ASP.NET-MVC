using Hospital.BusinessLogicLayer.Interfaces;
using Hospital.BusinessLogicLayer.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BusinessLogicLayer.Util
{
    public class UserServiceRegistration : NinjectModule
    {        
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();
        }
    }
}
