using Hospital.DataAccessLayer.Entities.ExtraModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DataAccessLayer.Entities
{
    public class MedicalCard
    {
        MedicalCard()
        {
            CardPages = new HashSet<MedicalCardPage>();
        }
        public int Id { set; get; }
        public DateTime CreatingDate { set; get; }

        public ICollection<MedicalCardPage> CardPages { get; set; }
        public virtual Patient Patient { set; get; }
    }
}
