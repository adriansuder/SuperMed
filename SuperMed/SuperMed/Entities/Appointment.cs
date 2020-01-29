using System;

namespace SuperMed.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        public DateTime StartDateTime { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public string Description { get; set; }
        public string Review { get; set; }

        public virtual Patient Patient { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}