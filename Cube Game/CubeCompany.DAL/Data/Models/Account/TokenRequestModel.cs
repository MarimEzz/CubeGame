using System.ComponentModel.DataAnnotations;

namespace CubeGame.Data.Models.Account
{
    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
