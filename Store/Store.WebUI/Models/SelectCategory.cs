using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Domain.Concrete;

namespace Store.WebUI.Models
{
    public class SelectCategory
    {
        public IEnumerable<string> Categories { get; set; }
        public string input_category { get; set; }

        public SelectCategory(string input_categ) {
            ApplicationDbContext context = new ApplicationDbContext();
            Categories = context.Categories
               .Select(categ => categ.Type)
               .Distinct()
               .OrderBy(x => x);
            input_category = input_categ;
        }
    }
}