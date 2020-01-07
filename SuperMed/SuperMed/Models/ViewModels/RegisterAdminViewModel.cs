using System.ComponentModel.DataAnnotations;

namespace SuperMed.Models.ViewModels
{
    public class RegisterAdminViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
