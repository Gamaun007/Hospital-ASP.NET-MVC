using Hospital.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BusinessLogicLayer.DataTransferObjects
{
    public class ProfileDTO
    {
        public string Id { get; set; }

        //public string Email { get; set; }
        //public string Name { get; set; }
        //public string Address { get; set; }

        public UserDTO ApplicationUser { get; set; }
        public  PatientDTO Patient { get; set; }
        public  DoctorDTO Doctor { get; set; }

    }
}
