using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Domain.Entities;
using Store.WebUI.Services;
using Store.Domain.Abstract;

namespace Store.WebUI.Models
{
    public class SearchViewModel
    {
        public List<SearchProduct> Products { get; set; }
        public string searchString { get; set; }

        public SearchViewModel()
        {
            Products = new List<SearchProduct>();
        }

        public SearchViewModel(string SearchString)
        {
            searchString = SearchString;
            Products = SearchService.SearchLiteProducts(SearchString);
        }
    }
}