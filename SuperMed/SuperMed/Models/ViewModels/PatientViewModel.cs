using System.Collections.Generic;
using SuperMed.Models.Entities;

namespace SuperMed.Models.ViewModels
{
    public class PatientViewModel
    {
        public Patient Patient { get; set; }
        public List<Appointment> GetPastAppointments { get; set; }
        public List<Appointment> GetUpcommingAppointments { get; set; }
    }
}
