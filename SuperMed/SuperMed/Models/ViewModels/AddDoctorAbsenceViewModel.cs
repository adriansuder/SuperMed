using SuperMed.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SuperMed.Models.Entities;

namespace SuperMed.Models.ViewModels
{
    public class AddDoctorAbsenceViewModel
    {
        [Required(ErrorMessage = "Data nieobecności jest wymagana.")]
        [DataType(DataType.Date)]
        [NotBeforeToday(ErrorMessage = "Data nieobecności nieprawidłowa.")]
        public DateTime AbsenceDate { get; set; }

        [Required(ErrorMessage = "Krótki opis jest wymagany.")]
        public string AbsenceDescription { get; set; }
        public List<DoctorAbsence> doctorAbsences {get; set;}
    }
}

