using System.Collections.Generic;
using Store.Domain.Entities;

namespace Store.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public string CurrentSort { get; set; }
        public ProductsListViewModel()
        { }

    }
}