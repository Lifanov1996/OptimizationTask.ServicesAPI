using System.ComponentModel.DataAnnotations;

namespace ServicesAPI.Models.Users
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Логине не указан")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Пароль не указан")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }

    }
}
