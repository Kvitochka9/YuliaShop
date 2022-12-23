using System.ComponentModel.DataAnnotations;

namespace ShoeShop.Core.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email/Ім'я користувача/ ?)")]
        public string EmailOrUsername { get; set; }

        [Required]
        [Display(Name = "Пароль ?)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
