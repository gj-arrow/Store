using System.Collections.Generic;
using Store.Domain.Entities;
using Store.Domain.Abstract;
using System.Linq;
using Store.Domain.Concrete;

namespace Store.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        Product product = new Product();
        ApplicationDbContext context = new ApplicationDbContext();

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        public IEnumerable<Category> Categories
        {
            get { return context.Categories; }
        }

        public IEnumerable<ApplicationUser> Users
        {
            get { return context.Users; }
        }

        public void SaveProduct(Product product, string input_category)
        {
            if (product.ProductId == 0)
            {
                product.Category = context.Categories.Where(c => c.Type == input_category).FirstOrDefault();
                context.Products.Add(product);

            }
            else
            {
                Product dbEntry = context.Products.Find(product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Category = context.Categories.Where(c => c.Type == input_category).FirstOrDefault();
                    dbEntry.Price = product.Price;
                    dbEntry.Picture = product.Picture;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            Product dbEntry = context.Products.Find(productId);
            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public Product GetProduct(int productId)
        {
            Product product = context.Products.Include("Category").Where(p => p.ProductId == productId).FirstOrDefault();
            return product;
        }


        public void SaveCategory(Category category)
        {

            if (category.CategoryId == 0)
            {
                context.Categories.Add(category);
            }
            else
            {
                Category dbEntry = context.Categories.Where(c => c.CategoryId == category.CategoryId).FirstOrDefault();
                    if (dbEntry != null)
                        {
                             dbEntry.Type = category.Type;
                        }
             }
            context.SaveChanges();
        }

        public Category DeleteCategory(int categoryId)
        {
            Category dbEntry = context.Categories.Where(c => c.CategoryId == categoryId).FirstOrDefault();
            IEnumerable<Product> Products = context.Products.Include("Category").Where(p => p.Category.CategoryId == categoryId);
            if (dbEntry != null)
            {
                foreach(Product product in Products)
                    context.Products.Remove(product);

                context.Categories.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public Category GetCategory(int categoryId)
        {
            Category category = context.Categories.Where(p => p.CategoryId == categoryId).FirstOrDefault();
            return category;
        }

        public Category GetCategoryByType(string categoryType) {
            Category category = context.Categories.Where(p => p.Type == categoryType).FirstOrDefault();
            return category;
        }


        public ApplicationUser DeleteUser(string userEmail)
        {
            ApplicationUser user = context.Users.Where(u => u.Email == userEmail).FirstOrDefault();
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
            return user;
        }

        public ApplicationUser GetUser(string userEmail){
            ApplicationUser user = context.Users.Where(u => u.Email == userEmail).FirstOrDefault();
            return user;
        }
    }
}


