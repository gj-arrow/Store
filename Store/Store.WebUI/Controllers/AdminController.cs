using System.Web.Mvc;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Store.WebUI.Models;
using Store.WebUI.Services;

namespace Store.WebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public Category categ = new Category();
        public IProductRepository repository;
        public int pageSize = 20;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(string category=null, string sort = "SortByNameUp", int page = 1)
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

     
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        public ViewResult Edit(int productId)
        {
            Product product = repository.GetProduct(productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase file, string input_category = null)
        { 
            if (input_category == "default") ModelState.AddModelError("", "Пожалуйста, выберите категорию");

            else if (repository.Products.Where(p =>( p.Name == product.Name && p.ProductId != product.ProductId)).FirstOrDefault() != null)
            { ModelState.AddModelError("", "Извините, но продукт с таким названием уже существует"); }
            else if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0 && FileUpload.isPicture(file.FileName))
                {
                    string filename = FileUpload.getFilename(file.FileName);
                    file.SaveAs(Path.Combine(Server.MapPath("~/Images"),filename));
                    product.Picture = "../../Images/" + filename;
                }

                else
                    product.Picture = "../../Images/no_image.png";
                repository.SaveProduct(product, input_category);
                TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                product.Category = repository.Categories.Where(c => c.Type == input_category).FirstOrDefault();
            }
                // Что-то не так со значениями данных
                return View(product);
            
        }


        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Товар \"{0}\" был удален",
                    deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }

        public PartialViewResult CategorySection(string input_category)
        {
            SelectCategory model = new SelectCategory(input_category);
            return PartialView(model);
        }


        public ViewResult ListCategory()
        {
            return View(repository.Categories);
        }

        public ViewResult CreateCategory()
        {
            return View("CreateCategory", new Category());
        }
        public Category GetCategoryByType1(string categoryType)
        {
            Category category = repository.Categories.Where(p => p.Type == categoryType).FirstOrDefault();
            return category;
        }
        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            Category category_double = GetCategoryByType1(category.Type);       
            if (ModelState.IsValid && category_double == null)
            {
                repository.SaveCategory(category);
                TempData["message"] = string.Format("Kатегория \"{0}\" была успешно добавлена", category.Type);
                return RedirectToAction("ListCategory");
            }
            else
            {
                if (category_double != null) ModelState.AddModelError("", "Такая категория уже существует");
                // Что-то не так со значениями данных
                return View(category);
            }
        }

        public ViewResult EditCategory(int categoryId)
        {
            Category category = repository.GetCategory(categoryId);
            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            Category category_double = repository.GetCategoryByType(category.Type);
            if (category_double != null) ModelState.AddModelError("", "Такая категория уже существует");
            if (ModelState.IsValid && category_double == null) 
            {
                repository.SaveCategory(category);
                TempData["message"] = string.Format("Изменения в категории \"{0}\" были сохранены", category.Type);
                return RedirectToAction("ListCategory");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(category);
            }
        }

        public ActionResult DeleteCategory(int categoryId)
        {
            Category category = repository.GetCategory(categoryId);
            if (category != null)
            {
                return PartialView("DeleteCategory");
            }
            return RedirectToAction("ListCategory");
        }

        [HttpPost]
        public ActionResult DeleteCategory(int categoryId, string type)
        {
            Category deletedCategory = repository.DeleteCategory(categoryId);
            if (deletedCategory != null)
            {
                TempData["message"] = string.Format("Категория \"{0}\" была удалена", deletedCategory.Type);
            }
            return RedirectToAction("ListCategory");
        }


        public ViewResult ListUsers()
        {
            return View(repository.Users);
        }

        [HttpPost]
        public ActionResult DeleteUser(string userEmail)
        {
            IdentityUser deletedUser = repository.GetUser(userEmail);
            if (userEmail == "Admin@mail.ru")
            {
                TempData["Admin"] = string.Format("Администратора удалять нельзя!!!");
            }
            else if ((ModelState.IsValid) && (deletedUser != null))
            {
                deletedUser = repository.DeleteUser(userEmail);
                TempData["message"] = string.Format("Пользователь \"{0}\" был удален", deletedUser.Email);
            }
            return RedirectToAction("ListUsers");
        }




        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}