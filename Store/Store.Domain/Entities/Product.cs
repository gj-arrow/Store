using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Store.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductId { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название товара")]
        [StringLength(30, ErrorMessage = "Название  должно содержать от 3 до 30 символов", MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [StringLength(5000, ErrorMessage = "Описание должно содержать от 10 до 5000 символов", MinimumLength = 10)]
        [Required(ErrorMessage = "Пожалуйста, введите описание товара")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите допустимое значение цены товара")]
        [Range(0.1, 10000, ErrorMessage = "Пожалуйста, введите положительное значение для цены(1-10000 руб)")]
        [Display(Name = "Цена (руб)")]
        public float Price { get; set; }

        [Display(Name = "Картинка")]
        public string Picture { get; set; }

        [Display(Name = "Категория")]
        public Category Category { get; set; }
    }
    }
