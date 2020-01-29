using System;

namespace SuperMed.Entities
{
    public class DoctorAbsence
    {
        public int Id { get; set; }
        
        public DateTime AbsenceDate { get; set; }
        public string AbsenceDescription { get; set; }
        
        public virtual Doctor Doctor { get; set; }
    }
}
