using SuperMed.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace SuperMed.Models.ViewModels
{
    public class CreateVisitStep3ViewModel
    {
        [Required]
        public Doctor Doctor { get; set; }

        public string DoctorName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDateTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime TimeOfDay { get; set; }

        public string Description { get; set; }
    }
}