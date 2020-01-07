using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SuperMed.Auth;
using SuperMed.DAL;

namespace SuperMed.Models.Entities
{
    public class Address : IEntity
    {   
        public int AddressId { get; set; }
        [ForeignKey("Doctor")]
        public int? DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
        [ForeignKey("Patient")]
        public int? PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public int StreetNumber { get; set; }
        public int? PropertyNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }


    }
}
