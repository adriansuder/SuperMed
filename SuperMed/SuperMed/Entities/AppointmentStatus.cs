using System.ComponentModel.DataAnnotations;

namespace SuperMed.Entities
{
    public enum AppointmentStatus
    {
        [Display(Name = "Nowa")]
        New,

        [Display(Name = "Zakończona")]
        Finished
    }
}