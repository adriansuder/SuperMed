using System;
using System.ComponentModel.DataAnnotations;

namespace SuperMed.Entities
{
    public class DoctorAbsence
    {
        [Key]
        public int Id { get; set; }
        
        public DateTime AbsenceDate { get; set; }
        public string AbsenceDescription { get; set; }
        
        public virtual Doctor Doctor { get; set; }
    }
}
