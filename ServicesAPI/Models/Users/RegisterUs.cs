using System.ComponentModel.DataAnnotations;

namespace ServicesAPI.Models.Users
{
    public class RegisterUs
    {
        [Required]
        public string? Username { get; set; }

        [EmailAddress]
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
