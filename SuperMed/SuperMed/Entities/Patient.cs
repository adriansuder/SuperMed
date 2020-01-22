using System;
using System.Collections.Generic;
using SuperMed.Auth;

namespace SuperMed.Entities
{
    public class Patient
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}