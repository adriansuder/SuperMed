using SuperMed.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMed.Models.ViewModels
{
    public class DoctorAppointmentHistoryViewModel
    {
        public List<Appointment> RealizedAppointments { get; set; }
    }
}
