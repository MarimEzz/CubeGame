using CubeGame.DAL.Data.Models.Account;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CubeGame.Data.Models.Account
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        public string Role { get; set; }

        public string ? token { get; set; }

        public List<RefreshToken>? RefreshTokens { get; set; }
    }
}
