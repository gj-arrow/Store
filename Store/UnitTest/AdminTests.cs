using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.WebUI.Controllers;
using Store.WebUI.Models;
using Store.WebUI.HtmlHelpers;

namespace UnitTest
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void List1()
        {

            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Продукт1", Price=100},
                new Product { ProductId = 2, Name = "Продукт2", Price=150},
                new Product { ProductId = 3, Name = "Продукт3", Price=200},
                new Product { ProductId = 4, Name = "Продукт4", Price=99},
                new Product { ProductId = 5, Name = "Продукт5", Price=1033}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);
            controller.pageSize = 9;

            // Действие
            ProductsListViewModel res = (ProductsListViewModel)controller.Index(null, "SortByNameUp", 1).Model;
            List<Product> result = res.Products.ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Продукт1", result[0].Name);
            Assert.AreEqual("Продукт2", result[1].Name);
            Assert.AreEqual("Продукт3", result[2].Name);
        }

        [TestMethod]
        public void List2()
        {

            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Продукт1", Price=100},
                new Product { ProductId = 2, Name = "Продукт2", Price=150},
                new Product { ProductId = 3, Name = "Продукт3", Price=200},
                new Product { ProductId = 4, Name = "Продукт4", Price=99},
                new Product { ProductId = 5, Name = "Продукт5", Price=1033}
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);
            controller.pageSize = 9;

            // Действие
            ProductsListViewModel res = (ProductsListViewModel)controller.Index(null, "SortByPriceUp", 1).Model;
            List<Product> result = res.Products.ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Продукт4", result[0].Name);
            Assert.AreEqual("Продукт1", result[1].Name);
            Assert.AreEqual("Продукт2", result[2].Name);
        }


        [TestMethod]
        public void List3()
        {

            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Продукт1", Price=100, Category =  new Category { CategoryId = 1, Type = "Витамины" } },
                new Product { ProductId = 2, Name = "Продукт2", Price=150, Category =  new Category { CategoryId = 1, Type = "Противокашлевые" } },
                new Product { ProductId = 3, Name = "Продукт3", Price=200, Category =  new Category { CategoryId = 1, Type = "Для носа" } },
                new Product { ProductId = 4, Name = "Продукт4", Price=99, Category =  new Category { CategoryId = 1, Type = "От аллергии" } },
                new Product { ProductId = 5, Name = "Продукт5", Price=1033, Category =  new Category { CategoryId = 1, Type = "Для глаз" } },
            });

            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);
            controller.pageSize = 9;

            // Действие
            ProductsListViewModel res = (ProductsListViewModel)controller.Index(null, "SortByCategoryUp", 1).Model;
            List<Product> result = res.Products.ToList();

            // Утверждение
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Продукт1", result[0].Name);
            Assert.AreEqual("Продукт5", result[1].Name);
            Assert.AreEqual("Продукт3", result[2].Name);
        }

        [TestMethod]
        public void DeleteCategory1()
        {
            // Организация - создание объекта
            Category category = new Category { CategoryId = 6, Type = "Вита" };

            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Продукт1", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 2, Name = "Продукт2", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 3, Name = "Продукт3", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 4, Name = "Продукт4", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 5, Name = "Продукт5", Category = new Category { CategoryId = 3, Type= "От кашля" } },
            });
            mock.Setup(m => m.Categories).Returns(new List<Category> {
               new Category { CategoryId = 1, Type= "Витамины"},
               new Category { CategoryId = 2, Type= "От аллергии"},
               new Category { CategoryId = 3, Type= "От кашля"},
               new Category { CategoryId = 4, Type= "Противовирусное"}
            });
            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            // Действие - удаление
            Category categorydeleted = controller.repository.DeleteCategory(6);
            Assert.AreEqual(null, categorydeleted);
        }


        [TestMethod]
        public void DeleteCategory2()
        {
            // Организация - создание объекта
            Category catеgory = null;
            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Продукт1", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 2, Name = "Продукт2", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 3, Name = "Продукт3", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 4, Name = "Продукт4", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 5, Name = "Продукт5", Category = new Category { CategoryId = 3, Type= "От кашля" } },
            });
            mock.Setup(m => m.Categories).Returns(new List<Category> {
               new Category { CategoryId = 1, Type= "Витамины"},
               new Category { CategoryId = 2, Type= "От аллергии"},
               new Category { CategoryId = 3, Type= "От кашля"},
               new Category { CategoryId = 4, Type= "Противовирусное"}
            });
            Category category = new Category() { CategoryId = 4, Products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Продукт1", Category = new Category { CategoryId = 4, Type= "Противовирусное" } },
                new Product { ProductId = 2, Name = "Продукт2", Category = new Category { CategoryId = 4, Type= "Противовирусное" } },
                new Product { ProductId = 3, Name = "Продукт3", Category = new Category { CategoryId = 4, Type= "Противовирусное" } },},
                Type = "Противовирусное" };
            // Организация - создание 
            AdminController controller = new AdminController(mock.Object);

            // Действие - удаление
            //   controller.DeleteCategory(category.CategoryId,"Витамины");

            Category categorydeleted = controller.repository.DeleteCategory(4);
            Assert.AreEqual(catеgory, categorydeleted);
        }

        [TestMethod]
        public void DeleteCategory3()
        {
            // Организация - создание объекта
            Category catеgory = null;

            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Продукт1", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 2, Name = "Продукт2", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 3, Name = "Продукт3", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 4, Name = "Продукт4", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 5, Name = "Продукт5", Category = new Category { CategoryId = 3, Type= "От кашля" } },
            });
            mock.Setup(m => m.Categories).Returns(new List<Category> {
               new Category { CategoryId = 1, Type= "Витамины"},
               new Category { CategoryId = 2, Type= "От аллергии"},
               new Category { CategoryId = 3, Type= "От кашля"},
               new Category { CategoryId = 4, Type= "Противовирусное"}
            });
            Category category = new Category() { CategoryId = 4, Products = null, Type = "Противовирусное" };
            // Организация - создание 
            AdminController controller = new AdminController(mock.Object);

            // Действие - удаление
            Category categorydeleted = controller.repository.DeleteCategory(4);
            Assert.AreEqual(catеgory, categorydeleted);
        }


        [TestMethod]
        public void CreateCategory1()
        {
            // Организация - создание объекта
            Category category = new Category { CategoryId = 6, Type = "Для глаз" };

            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Продукт1", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 2, Name = "Продукт2", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 3, Name = "Продукт3", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 4, Name = "Продукт4", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 5, Name = "Продукт5", Category = new Category { CategoryId = 3, Type= "От кашля" } },
            });
            mock.Setup(m => m.Categories).Returns(new List<Category> {
               new Category { CategoryId = 1, Type= "Витамины"},
               new Category { CategoryId = 2, Type= "От аллергии"},
               new Category { CategoryId = 3, Type= "От кашля"},
               new Category { CategoryId = 4, Type= "Противовирусное"}
            });
            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);

            RedirectToRouteResult result = controller.CreateCategory(category) as RedirectToRouteResult;
           // ActionResult result = controller.CreateCategory(category);
            Assert.AreEqual("ListCategory", result.RouteValues["action"]);
        }


        [TestMethod]
        public void CreateCategory2()
        {
            // Организация - создание объекта
            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Продукт1", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 2, Name = "Продукт2", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 3, Name = "Продукт3", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 4, Name = "Продукт4", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 5, Name = "Продукт5", Category = new Category { CategoryId = 3, Type= "От кашля" } },
            });
            mock.Setup(m => m.Categories).Returns(new List<Category> {
               new Category { CategoryId = 1, Type= "Витамины"},
               new Category { CategoryId = 2, Type= "От аллергии"},
               new Category { CategoryId = 3, Type= "От кашля"},
               new Category { CategoryId = 4, Type= "Противовирусное"}
            });
            Category category = new Category()
            {
                CategoryId = 4,
                Type = "Противовирусное"
            };
            // Организация - создание 
            AdminController controller = new AdminController(mock.Object);

            ViewResult result = controller.CreateCategory(category) as ViewResult;
            Assert.AreEqual(category, result.Model);
        }


        [TestMethod]
        public void CreateCategory3()
        {
            // Организация - создание объекта
            Category category = new Category { CategoryId = 6, Type = "Для глаз" };

            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Продукт1", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 2, Name = "Продукт2", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 3, Name = "Продукт3", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 4, Name = "Продукт4", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 5, Name = "Продукт5", Category = new Category { CategoryId = 3, Type= "От кашля" } },
            });
            mock.Setup(m => m.Categories).Returns(new List<Category> {
               new Category { CategoryId = 1, Type= "Витамины"},
               new Category { CategoryId = 2, Type= "От аллергии"},
               new Category { CategoryId = 3, Type= "От кашля"},
               new Category { CategoryId = 4, Type= "Противовирусное"}
            });
            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);
            controller.ModelState.AddModelError("","Error");
            ViewResult result = controller.CreateCategory(category) as ViewResult;
            Assert.AreEqual(category, result.Model);
        }


        [TestMethod]
        public void Edit1()
        {
            // Организация - создание объекта
            Product product = new Product { ProductId = 6, Name = "Продукт1" };

            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Продукт1", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 2, Name = "Продукт2", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 3, Name = "Продукт3", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 4, Name = "Продукт4", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 5, Name = "Продукт5", Category = new Category { CategoryId = 3, Type= "От кашля" } },
            });
            mock.Setup(m => m.Categories).Returns(new List<Category> {
               new Category { CategoryId = 1, Type= "Витамины"},
               new Category { CategoryId = 2, Type= "От аллергии"},
               new Category { CategoryId = 3, Type= "От кашля"},
               new Category { CategoryId = 4, Type= "Противовирусное"}
            });
            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);
            ViewResult result = controller.Edit(product, "default" ) as ViewResult;
            Assert.AreEqual(product, result.Model);
        }

        [TestMethod]
        public void Edit2()
        {
            // Организация - создание объекта
            Product product = new Product { ProductId = 6, Name = "Продукт1" };

            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Продукт1", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 2, Name = "Продукт2", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 3, Name = "Продукт3", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 4, Name = "Продукт4", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 5, Name = "Продукт5", Category = new Category { CategoryId = 3, Type= "От кашля" } },
            });
            mock.Setup(m => m.Categories).Returns(new List<Category> {
               new Category { CategoryId = 1, Type= "Витамины"},
               new Category { CategoryId = 2, Type= "От аллергии"},
               new Category { CategoryId = 3, Type= "От кашля"},
               new Category { CategoryId = 4, Type= "Противовирусное"}
            });
            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);
            //controller.ModelState.AddModelError("", "Error");
            ViewResult result = controller.Edit(product, "От простуды и гриппа") as ViewResult;
            Assert.AreEqual(product, result.Model);
        }


        [TestMethod]
        public void Edit3()
        {
            // Организация - создание объекта
            Product product = new Product { ProductId = 5, Name = "Продукт5" };

            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Продукт1", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 2, Name = "Продукт2", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 3, Name = "Продукт3", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 4, Name = "Продукт4", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 5, Name = "Продукт5", Category = new Category { CategoryId = 3, Type= "От кашля" } },
            });
            mock.Setup(m => m.Categories).Returns(new List<Category> {
               new Category { CategoryId = 1, Type= "Витамины"},
               new Category { CategoryId = 2, Type= "От аллергии"},
               new Category { CategoryId = 3, Type= "От кашля"},
               new Category { CategoryId = 4, Type= "Противовирусное"}
            });
            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);
            controller.ModelState.AddModelError("", "Error");
            ViewResult result = controller.Edit(product, "От простуды и гриппа") as ViewResult;
            Assert.AreEqual(product, result.Model);
        }

        [TestMethod]
        public void Edit4()
        {
            // Организация - создание объекта
            Product product = new Product { ProductId = 5, Name = "Продукт5" };

            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Продукт1", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 2, Name = "Продукт2", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 3, Name = "Продукт3", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 4, Name = "Продукт4", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 5, Name = "Продукт5", Category = new Category { CategoryId = 3, Type= "От кашля" } },
            });
            mock.Setup(m => m.Categories).Returns(new List<Category> {
               new Category { CategoryId = 1, Type= "Витамины"},
               new Category { CategoryId = 2, Type= "От аллергии"},
               new Category { CategoryId = 3, Type= "От кашля"},
               new Category { CategoryId = 4, Type= "Противовирусное"}
            });
            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);
            RedirectToRouteResult result = controller.Edit(product, "От простуды и гриппа") as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }


        [TestMethod]
        public void Edit5()
        {
            // Организация - создание объекта
            Product product = new Product { ProductId = 5, Name = "Продукт5" , Picture = "aa.png" };

            // Организация - создание имитированного хранилища данных
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product { ProductId = 1, Name = "Продукт1", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 2, Name = "Продукт2", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 3, Name = "Продукт3", Category = new Category { CategoryId = 1, Type= "Витамины" } },
                new Product { ProductId = 4, Name = "Продукт4", Category = new Category { CategoryId = 2, Type= "От аллергии" } },
                new Product { ProductId = 5, Name = "Продукт5", Category = new Category { CategoryId = 3, Type= "От кашля" } },
            });
            mock.Setup(m => m.Categories).Returns(new List<Category> {
               new Category { CategoryId = 1, Type= "Витамины"},
               new Category { CategoryId = 2, Type= "От аллергии"},
               new Category { CategoryId = 3, Type= "От кашля"},
               new Category { CategoryId = 4, Type= "Противовирусное"}
            });
            // Организация - создание контроллера
            AdminController controller = new AdminController(mock.Object);
            RedirectToRouteResult result = controller.Edit(product, "От простуды и гриппа") as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
