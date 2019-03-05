using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Areas.Profile.Models
{
    public class ProfileViewModel
    {
        public string Email { get; set; }      
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<string> Roles { get; set; }
        public bool IsUserPatient { get; set; }
        public bool IsUserDoctor { get; set; }
    }
}