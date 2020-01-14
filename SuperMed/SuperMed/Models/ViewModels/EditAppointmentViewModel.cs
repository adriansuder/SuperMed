using SuperMed.Models.Entities;

namespace SuperMed.Models.ViewModels
{
    public class EditAppointmentViewModel
    {
        public Appointment Appointment { get; set; }

        public bool IsDoctor { get; set; }
    }
}