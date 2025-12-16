using Microsoft.AspNetCore.Identity;

namespace JWTtokenDem.Models
{
    public class User:IdentityUser
    {
        public string Password { get; set; }
        public string City { get; set; }
    }
}
