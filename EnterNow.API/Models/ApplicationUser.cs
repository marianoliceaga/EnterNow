using Microsoft.AspNetCore.Identity;

namespace EnterNow.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime MembershipExpiry { get; set; }
        public bool IsPaymentCurrent { get; set; }
    }
}