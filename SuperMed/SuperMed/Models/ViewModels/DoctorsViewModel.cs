using System.Collections.Generic;
using SuperMed.Models.Entities;

namespace SuperMed.Models.ViewModels
{
    public class DoctorsViewModel
    {
        public List<Appointment> Appointments { get; set; }
        public List<DoctorAbsence> NextDoctorsAbsences { get; set; }
    }
}
