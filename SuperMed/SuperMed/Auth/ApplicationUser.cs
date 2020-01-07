using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using SuperMed.Models.Entities;

namespace SuperMed.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public bool? IsActive { get; set; }
        
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; } = new List<IdentityUserClaim<string>>();

    }
}
