using Microsoft.AspNetCore.Mvc.Rendering;
using SuperMed.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperMed.Models.ViewModels
{
    public class CreateVisitViewModel
    {
        [Required]
        public string DoctorName { get; set; }

        [Required(ErrorMessage = "Krótki opis jest wymagany.")]
        public string Description { get; set; }

        public IEnumerable<SelectListItem> Doctors { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [NotBeforeToday(ErrorMessage = "Niepoprawna data planowanej wizyty.")]
        public DateTime StartDateTime { get; set; }
    }
}