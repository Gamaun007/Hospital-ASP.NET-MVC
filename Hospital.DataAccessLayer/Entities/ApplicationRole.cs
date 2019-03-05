using Microsoft.AspNet.Identity.EntityFramework;

namespace Hospital.DataAccessLayer.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string roleName)
            : base(roleName) { }

        public ApplicationRole()
            : base() { }
    }
}
