using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.WebUI.Models;
using Store.WebUI.SearchLucene;
using Store.Domain.Concrete;


namespace Store.WebUI.Services
{
    public class SearchService
    {
        public SearchService()
        {
        }

        internal static List<SearchProduct> SearchLiteProducts(string searchString)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var productSearcher = new SearchProducts();
            productSearcher.ClearLuceneIndex();
            productSearcher.AddUpdateLuceneIndex(context.Products.Include("Category").ToList());
            return productSearcher.Search(searchString).ToList();
        }


    }
}