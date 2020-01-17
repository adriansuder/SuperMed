using System.ComponentModel.DataAnnotations;

namespace SuperMed.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Podaj login.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Podaj hasło.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
