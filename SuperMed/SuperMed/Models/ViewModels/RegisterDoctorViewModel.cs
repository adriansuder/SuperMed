using System.ComponentModel.DataAnnotations;

namespace SuperMed.Models.ViewModels
{
    public class RegisterDoctorViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Specialization { get; set; }
    }
}
