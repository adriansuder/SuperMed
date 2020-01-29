using SuperMed.Auth;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperMed.Entities
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<DoctorAbsence> Absences { get; set; }
    }
}
