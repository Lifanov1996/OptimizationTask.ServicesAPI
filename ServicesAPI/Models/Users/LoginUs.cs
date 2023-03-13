using System.ComponentModel.DataAnnotations;

namespace ServicesAPI.Models.Users
{
    public class LoginUs
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
