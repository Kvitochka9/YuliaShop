using System.ComponentModel.DataAnnotations;

namespace ShoeShop.Core.Dto
{
    public class ShoeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Одмен, що це за шузи?")]
        [Display(Name = "Назва шузів")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Ну хоч два слова!")]
        [Display(Name = "Короткий опис")]
        [MaxLength(50)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Покупцям важливо знати інфу!")]
        [Display(Name = "Опис")]
        [MaxLength(255)]
        public string LongDescription { get; set; }

        [Required(ErrorMessage = "ХАВ МАЧ ЦІ ШУЗА КОШТУЮТЬ?")]
        [Display(Name = "Прайс")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Їх мусять побачити!")]
        [Display(Name = "Url зображення")]
        public string ImageUrl { get; set; }

        [Display(Name = "Вибір тижня? ")]
        public bool IsShoeOfTheWeek { get; set; }

        [Required(ErrorMessage = "А категорія?)")]
        [Display(Name = "Категорія")]
        public int CategoryId { get; set; }
    }
}
