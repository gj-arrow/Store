using System.Web.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Store.Domain.Entities
{
    public class Category
    {
        [HiddenInput(DisplayValue = false)]
        public int CategoryId { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Пожалуйста, укажите категорию для товара")]
        [StringLength(40, ErrorMessage = "Название категории должно содержать от 3 до 40 символов", MinimumLength = 3)]
        public string Type { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
