using SuperMed.DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperMed.Models.Entities
{
    public class Specialization : IEntity
    {
        [Key, ForeignKey("Specialization")]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}