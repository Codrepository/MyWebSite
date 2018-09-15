using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopingWebSite.Models;
using System.Web.Helpers;

namespace ShopingWebSite.Controllers
{
    public class ProductController : Controller
    {
        int pagesize = 5;
        PersianDateTime time = PersianDateTime.Now;
        DatabaseContext context = new DatabaseContext();
        // GET: Product
        public ActionResult List(string categoryName, int page = 1)
        {
            ViewBag.Time = time;
            ProductListViewModel model = new ProductListViewModel();
            IEnumerable<Product> products = null;
            if (categoryName == null || categoryName == "")
            {
                products = context.Products.OrderBy(p => p.Id);
            }
            else
            {
                Category cat = context.Categories.Where(x => x.Name == categoryName).FirstOrDefault();
                if (cat != null)
                {
                    products = context.Products.Where(x => x.Category.Id == cat.Id);
                    model.Category = cat;
                }


            }
            PagingInfo info = new PagingInfo { TotalItems = products.Count(), ItemsPerPage = pagesize, CurrentPage = page };
            products = products.Skip((page - 1) * pagesize).Take(pagesize);
            model.Products = products;
            model.PageInfo = info;

            return View(model);
        }
        
        public PartialViewResult ProductSummary(Product product)
        {
            var pictures = context.Pictures.Where(x => x.Product.Id == product.Id);
            ProductSummaryViewModel model = new ProductSummaryViewModel { Product = product, Pictures = pictures };
            return PartialView("ProductSummary",model);
        }
    }
}