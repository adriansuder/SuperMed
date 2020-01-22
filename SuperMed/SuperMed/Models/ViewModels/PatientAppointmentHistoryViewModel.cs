using System.Collections.Generic;
using SuperMed.Entities;

namespace SuperMed.Models.ViewModels
{
    public class PatientAppointmentHistoryViewModel
    {
        public List<Appointment> RealizedAppointments { get; set; }
    }
}
