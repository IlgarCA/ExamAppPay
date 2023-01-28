using Microsoft.AspNetCore.Identity;

namespace AppPay.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
