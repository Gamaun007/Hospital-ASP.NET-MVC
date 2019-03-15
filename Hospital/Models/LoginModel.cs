using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Login is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.Password)]
        public string Password { set; get; }
    }
}