using Microsoft.AspNet.Identity.EntityFramework;

namespace Hospital.DataAccessLayer.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { set; get;}
        //public string FirstName { set; get; }
        //public string SecondName { set; get; }
        public string Address { get; set; }
        public virtual UserProfile Profile { set; get; }
    }
}
