using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Domain.Entities;

namespace Store.WebUI.Models
{
    public class SearchProduct
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public float  Price { get; set; }
        public string Picture { get; set; }
        public string Category { get; set; }

        public SearchProduct(Product product) {
            ProductId = product.ProductId;
            Name = product.Name;
            Discription = product.Description;
            Price = product.Price;
            Picture = product.Picture;
            Category = product.Category.Type;
        }
    }
}