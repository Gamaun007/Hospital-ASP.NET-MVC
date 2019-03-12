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
        public MedicalCard()
        {
            Pages = new HashSet<MedicalCardPage>();
            CreatingDate = DateTime.Now;
        }
        public int Id { set; get; }
        public DateTime CreatingDate { set; get; }

        public ICollection<MedicalCardPage> Pages { get; set; }
        public virtual Patient Patient { set; get; }
    }
}
