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
    /// <summary>
    /// Summary description for CartTests
    /// </summary>
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Add_To_Cart()
        {
            // Организация - создание имитированного хранилища
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product {ProductId = 1, Name = "Продукт1"},
                 new Product {ProductId = 2, Name = "Продукт2"}
            });

            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - создание контроллера
            CartController controller = new CartController(mock.Object, null);

            // Действие - добавить игру в корзину
            controller.AddToCart(cart, 1, null);
            controller.AddToCart(cart, 2, null);

            // Утверждение
            Assert.AreEqual(2, cart.Lines.Count());
            Assert.AreEqual(cart.Lines.ToList()[0].Product.ProductId, 1);
        }


        [TestMethod]
        public void Add_To_Cart2()
        {
            // Организация - создание имитированного хранилища
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product> {
                new Product {ProductId = 1, Name = "Продукт1"},
                 new Product {ProductId = 2, Name = "Продукт2"}
            });

            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - создание контроллера
            CartController controller = new CartController(mock.Object, null);

            controller.AddToCart(cart, 1, null);
            controller.AddToCart(cart, 2, null);
            controller.AddToCart(cart, 3, null);

            // Утверждение
            Assert.AreEqual(2,cart.Lines.Count());
        }
    }
}