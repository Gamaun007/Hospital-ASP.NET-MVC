using System.Web.Mvc;

namespace Hospital.Areas.Profile
{
    public class ProfileAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Profile";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Profile_default",
                "Profile/{controller}/{action}/{id}",
                new { action = "Index", controller = "Information", id = UrlParameter.Optional }
            );

        }
    }
}