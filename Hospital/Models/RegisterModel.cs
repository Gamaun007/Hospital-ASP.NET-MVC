using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class RegisterModel
    {

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Second Name is required")]
        [Display(Name = "Second Name")]
        public string SecondName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone Number not correct to format")]
        [Display(Name = "Phone Name")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Birth Date is required")]
        [DataType(DataType.Date, ErrorMessage = "{0} {1}Birth date not correct to format")]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password,ErrorMessage = "Passwords do not match")]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Address Date is required")]
        public string Address { get; set; }
       
    }
}