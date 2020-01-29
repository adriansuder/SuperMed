using SuperMed.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SuperMed.Models.ViewModels
{
    public class RegisterPatientViewModel
    {
        [Required(ErrorMessage = "Podaj swój login.")]
        [StringLength(20, ErrorMessage = "Nazwa użytkownika musi zawierać {2}-{1} znaków", MinimumLength = 6)]
        [RegularExpression("[a-zA-Z0-9]*[^!@%~?:;#$%^&*()/\"']*",
            ErrorMessage = "Login może składać się z wielkich i małych liter oraz cyfr.")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Imię jest wymagane.")]
        [StringLength(40, ErrorMessage = "Imię musi składać się przynajmniej z {2} znaków", MinimumLength = 3)]
        [RegularExpression("[a-zA-Z-]*[^!@%~?:;#$%^&*()/\"0-9']*",
            ErrorMessage = "Imię może składać się z wielkich i małych liter.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [StringLength(40, ErrorMessage = "Nazwisko musi składać się przynajmniej z {2} znaków", MinimumLength = 2)]
        [RegularExpression("[a-zA-Z-]*[^!@%~?:;#$%^&*()/\"0-9']*",
            ErrorMessage = "Nazwisko może składać się z wielkich i małych liter.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-mail jest wymagany.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Phone]
        [Required(ErrorMessage = "Numer telefonu jest wymagany.")] 
        [RegularExpression(@"([0-9]{9})$", 
            ErrorMessage = "Numer telefonu musi być zapisany w formacie 9 znaków 0-9")]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = "Data urodzenia jest wymagana.")]
        [DataType(DataType.Date, ErrorMessage = "Nieprawidłowy format daty.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Proszę wybrać płeć.")]
        public Gender Gender { get; set; }
    }
}