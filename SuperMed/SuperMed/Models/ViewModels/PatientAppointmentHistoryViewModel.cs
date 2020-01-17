using SuperMed.Models.Entities;
using System.Collections.Generic;

namespace SuperMed.Models.ViewModels
{
    public class PatientAppointmentHistoryViewModel
    {
        public List<Appointment> RealizedAppointments { get; set; }
    }
}
