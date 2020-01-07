using System;
using SuperMed.DAL;

namespace SuperMed.Models.Entities
{
    public class Appointment : IEntity
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }

        public Status Status { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public string Description { get; set; }
    }

    public enum Status
    {
        New,
        Finished
    }
}