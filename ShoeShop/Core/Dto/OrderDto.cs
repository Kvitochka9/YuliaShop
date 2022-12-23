using System.ComponentModel.DataAnnotations;

namespace ShoeShop.Core.Dto
{
    public class OrderDto
    {


        [StringLength(255)]
        [Display(Name = "Ім'я")]
        [Required(ErrorMessage = "Без ім'я нікуда :)")]
        public string FirstName { get; set; }

        [StringLength(255)]
        [Display(Name = "Прізвище")]
        [Required(ErrorMessage = "і без прізвища нікуда :)")]
        public string LastName { get; set; }

        [StringLength(255)]
        [Display(Name = "Адреса 1")]
        [Required(ErrorMessage = "Необхідна хоч одна адреса")]
        public string AddressLine1 { get; set; }

        [StringLength(255)]
        [Display(Name = "Адреса 2")]
        public string AddressLine2 { get; set; }

        [StringLength(255)]
        [Display(Name = "Місто")]
        [Required(ErrorMessage = "А як ми вас знайдемо?")]
        public string City { get; set; }

        [StringLength(255)]
        [Display(Name = "Область")]
        [Required(ErrorMessage = "Область ще, будь ласка")]
        public string State { get; set; }

        [StringLength(255)]
        [Display(Name = "Країна")]
        [Required(ErrorMessage = "Ну куди ж без країни ?")]
        public string Country { get; set; }

        [StringLength(6)]
        [Required(ErrorMessage = "Ви не хочете отримати свої нові шузи ?")]
        [Display(Name = "Поштовий індекс")]
        public string ZipCode { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Не кажіть, що не маєте :)")]
        [Display(Name = "Мобільний номер")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "А куди квинтацію прислати ?")]
        [Display(Name = "Email адреса")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
