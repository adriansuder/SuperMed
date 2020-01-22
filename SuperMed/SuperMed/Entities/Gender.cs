using System.ComponentModel.DataAnnotations;

namespace SuperMed.Entities
{
    public enum Gender
    {
        [Display(Name="mężczyzna")]
        Male,

        [Display(Name = "kobieta")]
        Female
    }
}