using System.ComponentModel.DataAnnotations;

namespace SuperMed.Entities
{
    public class Specialization
    {
        [Key]
        public int SpecializationId { get; set; }

        public string Name { get; set; }
    }
}