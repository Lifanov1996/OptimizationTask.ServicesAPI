using System.ComponentModel.DataAnnotations;

namespace ServicesAPI.Models.Users
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }


        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }

    }
}
