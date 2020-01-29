using System;
using System.ComponentModel.DataAnnotations;

namespace SuperMed.Entities
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartDateTime { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public string Description { get; set; }
        public string Review { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}