using System.ComponentModel.DataAnnotations;

namespace SuperMed.Models.ViewModels
{
    public class LoginPatientViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
