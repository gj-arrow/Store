using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Укажите как вас зовут")]
        [Display(Name = "Ваше имя")]
        [StringLength(30, ErrorMessage = "Имя  должно содержать от 3 до 50 символов", MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        [Display(Name = "Город")]
        [StringLength(30, ErrorMessage = "Название  должно содержать от 3 до 50 символов", MinimumLength = 1)]
        public string City { get; set; }

        [Required(ErrorMessage = "Укажите название улицы")]
        [Display(Name = "Улица")]
        [StringLength(30, ErrorMessage = "Название  должно содержать от 3 до 50 символов", MinimumLength = 1)]
        public string Street { get; set; }

        [Required(ErrorMessage = "Укажите номер дома")]
        [Display(Name = "Дом")]
        [StringLength(30, ErrorMessage = "Название  должно содержать от 3 до 50 символов", MinimumLength = 1)]
        public string House { get; set; }

        [Required(ErrorMessage = "Укажите номер квартиры")]
        [Display(Name = "Квартира")]
        [StringLength(30, ErrorMessage = "Название  должно содержать от 3 до 50 символов", MinimumLength = 1)]
        public string Flat { get; set; }
    }
}