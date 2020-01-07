using System.Collections.Generic;
using SuperMed.Auth;
using SuperMed.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperMed.Models.Entities
{
    public class Doctor : IEntity
    {
        [ForeignKey("Address")]
        public int DoctorId { get; set; }
        
        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }
        
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public string ApplicationUserID { get; set; }
        public ApplicationUser AplicationUser { get; set; }

        public List<Appointment> Appointments { get; set; }

        public Doctor()
        {
            Appointments = new List<Appointment>();
        }
    }
}
