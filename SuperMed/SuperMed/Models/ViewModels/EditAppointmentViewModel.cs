using System.ComponentModel.DataAnnotations;
using SuperMed.Entities;

namespace SuperMed.Models.ViewModels
{
    public class EditAppointmentViewModel
    {
        [Required]
        public Appointment Appointment { get; set; }

        public bool IsDoctor { get; set; }
    }
}