using Hospital.DataAccessLayer.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospital.DataAccessLayer.Entities
{
    public class Doctor
    {
        public Doctor()
        {
            Patients = new HashSet<Patient>();
        }

        public int Id { set; get; }
        public bool IsConfirmed { set; get; }

        [Required]
        public Specialization Specialization { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public virtual UserProfile Profile { get; set; }
    }
}
