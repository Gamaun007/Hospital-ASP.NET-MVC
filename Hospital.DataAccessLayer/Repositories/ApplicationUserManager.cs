using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.DataAccessLayer.Entities;
using Microsoft.AspNet.Identity;

namespace Hospital.DataAccessLayer.Repositories
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {

        public ApplicationUserManager(IUserStore<ApplicationUser> store)
                : base(store) { }
        
    }
}
