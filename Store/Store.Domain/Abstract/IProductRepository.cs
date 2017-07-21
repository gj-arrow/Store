using System;
using System.Collections.Generic;
using Store.Domain.Entities;
using Store.Domain.Concrete;

namespace Store.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        IEnumerable<Category> Categories { get; }
        IEnumerable<ApplicationUser> Users { get; }
        void SaveProduct(Product product, string input_category);
        Product DeleteProduct(int productId);
        Product GetProduct(int productId);
        void SaveCategory(Category category);
        Category DeleteCategory(int categoryId);
        Category GetCategory(int categoryId);
        Category GetCategoryByType(string categoryType);
        ApplicationUser DeleteUser(string userId);
        ApplicationUser GetUser(string userId);
    }
}

