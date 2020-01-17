using SuperMed.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace SuperMed.Models.ViewModels
{
    public class AddDoctorAbsenceViewModel
    {
        [Required(ErrorMessage = "Data nieobecności jest wymagana.")]
        [NotBeforeToday(ErrorMessage = "Data nieobecności nieprawidłowa.")]
        [DataType(DataType.Date)]
        public DateTime AbsenceDate { get; set; }

        [Required(ErrorMessage = "Krótki opis jest wymagany.")]
        public string AbsenceDescription { get; set; }
    }
}

