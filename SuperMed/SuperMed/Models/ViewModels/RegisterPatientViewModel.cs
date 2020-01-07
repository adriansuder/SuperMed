using System;
using System.ComponentModel.DataAnnotations;
using SuperMed.Models.Entities;

namespace SuperMed.Models.ViewModels
{
    public class RegisterPatientViewModel
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
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Please select Gender")]
        public Gender Gender { get; set; }
    }
}