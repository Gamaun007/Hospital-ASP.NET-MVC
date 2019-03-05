using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Hospital.BusinessLogicLayer.Services;
using Microsoft.AspNet.Identity;
using Hospital.BusinessLogicLayer.Interfaces;
using Hospital;
using Ninject;
using Hospital.App_Start;

[assembly: OwinStartup(typeof(UserStore.App_Start.Startup))]

namespace UserStore.App_Start
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(() => ApplicationKernel.Kernel.Get<IUserService>());
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}