using SuperMed.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SuperMed.Models.ViewModels
{
    public class RegisterPatientViewModel
    {
        [Required(ErrorMessage = "Podaj swój login.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Imię jest wymagane.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-mail jest wymagany.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany.")] 
        public string Phone { get; set; }
        
        [Required(ErrorMessage = "Data urodzenia jest wymagana.")]
        [DataType(DataType.Date, ErrorMessage = "Nieprawidłowy format daty.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Proszę wybrać płeć.")]
        public Gender Gender { get; set; }
    }
}