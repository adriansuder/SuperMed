using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperMed.Models.Entities;

namespace SuperMed.Models.ViewModels
{
    public class AddDoctorAbsenceViewModel
    {

        [Required]
        public string AbsenceDescription { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime AbsenceDate { get; set; }

    }
}

