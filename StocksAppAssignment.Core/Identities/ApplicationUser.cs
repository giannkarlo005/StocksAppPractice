using Microsoft.AspNetCore.Identity;

namespace StocksAppAssignment.Core.Identities
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string? PersonName { get; set; }
    }
}
