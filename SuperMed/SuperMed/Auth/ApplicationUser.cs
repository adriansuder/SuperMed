using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SuperMed.Auth
{
    public sealed class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public bool? IsActive { get; set; }
        
        public ICollection<IdentityUserClaim<string>> Claims { get; } = new List<IdentityUserClaim<string>>();
    }
}
