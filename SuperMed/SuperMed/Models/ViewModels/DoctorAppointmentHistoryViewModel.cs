using System.Collections.Generic;
using SuperMed.Entities;

namespace SuperMed.Models.ViewModels
{
    public class DoctorAppointmentHistoryViewModel
    {
        public List<Appointment> RealizedAppointments { get; set; }
    }
}
