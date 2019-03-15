using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BusinessLogicLayer.DataTransferObjects
{
    public class AdminPanelDTO
    {
        public int PatientsInSystem { set; get; }
        public int PatientsConfirmed { set; get; }
        public int PatientsOnTreatment { set; get; }

        public int DoctorsTreat { set; get; }

        public int DoctorsInSystem { set; get; }
        public int DoctorsConfirmed { set; get; }

        public int Users { set; get; }
    }
}
