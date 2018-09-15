using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopingWebSite.Models;

namespace ShopingWebSite.Controllers
{
    public class CategoryController : Controller
    {
        DatabaseContext context = new DatabaseContext();
        // GET: Category
        public PartialViewResult Menu()
        {
            IEnumerable<string> categories = context.Categories.Select(x => x.Name).Distinct();

            return PartialView("Menu",categories);
        }
    }
}