using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SuperMed.Models.Entities;

namespace SuperMed.Models.ViewModels
{
    public class CreateVisitStep2ViewModel
    {
        [Required]
        public Doctor Doctor { get; set; }

        [Required]
        public string DoctorName { get; set; }

        public List<DateTime> Date { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDateTime { get; set; }

        [Required]
        public DateTime TimeOfDay { get; set; }

        public string Description { get; set; }
    }
}
