using SuperMed.DAL;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperMed.Models.Entities
{
    public class DoctorAbsence : IEntity
    {
        public int DoctorAbsenceId { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime AbsenceDate { get; set; }
        
        public string AbsenceDescription { get; set; }
        
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
