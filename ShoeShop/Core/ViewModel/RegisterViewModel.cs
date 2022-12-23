using System.ComponentModel.DataAnnotations;

namespace ShoeShop.Core.ViewModel
{
    public class RegisterViewModel
    {

        [Required]
        [Display(Name = "Ім'я користувача ?)")]
        public string UserName { get; set; }

        [Required]
        //[Display(Name = "текст")] таг дісплей не обяз
        [Display(Name = "Email ?)")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль ?)")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
