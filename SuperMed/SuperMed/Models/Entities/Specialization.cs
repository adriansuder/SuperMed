using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SuperMed.DAL;

namespace SuperMed.Models.Entities
{
    public class Specialization : IEntity
    {
        [Key, ForeignKey("Specialization")]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}