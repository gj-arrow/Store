using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.WebUI.Models;
using Store.Domain.Concrete;

namespace Store.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public Category categ = new Category();
        private IProductRepository repository;
        public int pageSize = 9;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Search(string searchstring = "")
        {
            var model = new SearchViewModel(searchstring);
            if (searchstring.Length <= 20)
            {
                if (model.Products.Count == 0)
                    ViewBag.Error = "По запросу \"" + searchstring + "\" ничего не найдено.";
                else
                    ViewBag.Error = "По запросу " + searchstring + " найдены следующие товары:";
            }
            else ViewBag.Error = "Запрос для поиска должен содержать не более 20 символов";
            return View(model);
        }


        public ViewResult List(string category, string sort = "SortByNameUp", int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel();
            if (sort == "SortByNameUp")
            {
                categ = repository.Categories.Where(c => c.Type == category).FirstOrDefault();
                model = new ProductsListViewModel
                {
                    Products = repository.Products
                             .Where(p => p.Category == categ || category == null)
                             .OrderBy(product => product.Name)
                             .Skip((page - 1) * pageSize)
                             .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ?
                             repository.Products.Count() :
                             repository.Products.Where(product => product.Category == categ).Count()
                    },
                    CurrentCategory = category,
                    CurrentSort = sort
                };
            }

            else if (sort == "SortByNameDown")
            {
                categ = repository.Categories.Where(c => c.Type == category).FirstOrDefault();
                model = new ProductsListViewModel
                {
                    Products = repository.Products
                       .Where(p => p.Category == categ || category == null)
                       .OrderByDescending(product => product.Name)
                       .Skip((page - 1) * pageSize)
                       .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ?
                       repository.Products.Count() :
                       repository.Products.Where(product => product.Category == categ).Count()
                    },
                    CurrentCategory = category,
                    CurrentSort = sort
                };
            }

            else if (sort == "SortByPriceDown")
            {
                categ = repository.Categories.Where(c => c.Type == category).FirstOrDefault();
                model = new ProductsListViewModel
                {
                    Products = repository.Products
                       .Where(p => p.Category == categ || category == null)
                       .OrderByDescending(product => product.Price)
                       .Skip((page - 1) * pageSize)
                       .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ?
                       repository.Products.Count() :
                       repository.Products.Where(product => product.Category == categ).Count()
                    },
                    CurrentCategory = category,
                    CurrentSort = sort
                };
            }

            else if (sort == "SortByPriceUp")
            {
                categ = repository.Categories.Where(c => c.Type == category).FirstOrDefault();
                model = new ProductsListViewModel
                {
                    Products = repository.Products
                       .Where(p => p.Category == categ || category == null)
                       .OrderBy(product => product.Price)
                       .Skip((page - 1) * pageSize)
                       .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ?
                       repository.Products.Count() :
                       repository.Products.Where(product => product.Category == categ).Count()
                    },
                    CurrentCategory = category,
                    CurrentSort = sort
                };
            }

            else if (sort == "SortByCategoryDown")
            {
                categ = repository.Categories.Where(c => c.Type == category).FirstOrDefault();
                model = new ProductsListViewModel
                {
                    Products = repository.Products
                       .Where(p => p.Category == categ || category == null)
                       .OrderByDescending(product => product.Category.Type)
                       .Skip((page - 1) * pageSize)
                       .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ?
                       repository.Products.Count() :
                       repository.Products.Where(product => product.Category == categ).Count()
                    },
                    CurrentCategory = category,
                    CurrentSort = sort
                };
            }
            else if (sort == "SortByCategoryUp")
            {
                categ = repository.Categories.Where(c => c.Type == category).FirstOrDefault();
                model = new ProductsListViewModel
                {
                    Products = repository.Products
                       .Where(p => p.Category == categ || category == null)
                       .OrderBy(product => product.Category.Type)
                       .Skip((page - 1) * pageSize)
                       .Take(pageSize),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = pageSize,
                        TotalItems = category == null ?
                       repository.Products.Count() :
                       repository.Products.Where(product => product.Category == categ).Count()
                    },
                    CurrentCategory = category,
                    CurrentSort = sort
                };
            }

            return View(model);

        }
        public ViewResult ViewProduct(int productId)
        {
            Product product = repository.GetProduct(productId);
            return View(product);
        }
    }
}