using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopingWebSite.Models;
using ShopingWebSite.DataAccessClasses;
namespace ShopingWebSite.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        DatabaseContext context = new DatabaseContext();
        
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult ShowProducts()
        {
            IEnumerable<AdminProductViewModel> p = context.Products.Select(x => 
            new AdminProductViewModel { Category = x.Category, Product = x });

            return View("Products", p);
                   
        }
        public ActionResult EditProduct( string returnUrl,int Id = 0)
        {
            var product = context.Products.FirstOrDefault(x => x.Id == Id) ?? new Product();
            AdminEditProductViewModel model = new AdminEditProductViewModel
            {
                List = context.Categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Name }),
                Product = product,
                ReturnUrl = returnUrl,
                Pictures = context.Pictures.Where(x=>x.Product.Id == product.Id),
            };
                
            return View(model);
        }
        [HttpPost]
        public ActionResult EditProduct(Product product,string returnUrl, string categoryName,HttpPostedFileBase Image =null)
        {
            if (ModelState.IsValid)
            {
                
                //use extention method to save product
                context.SaveProduct(product, categoryName);
                if (Image != null)
                {
                    var picture = new Picture();
                    picture.ImageMimeType = Image.ContentType;
                    picture.ImageData = new byte[Image.ContentLength];
                    Image.InputStream.Read(picture.ImageData, 0, Image.ContentLength);
                    //use extention method to save picture
                    context.SavePicture(picture, product);
                }
                return Redirect(returnUrl);
            }
            AdminEditProductViewModel model = new AdminEditProductViewModel
            {
                List = context.Categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Name }),
                Product = product,
                Pictures = context.Pictures.Where(x=>x.Product.Id == product.Id),
                };
            return View(model);
        }
        public ActionResult CreateProduct(string returnUrl)
        {
            return RedirectToAction("EditProduct",new { returnUrl});
        }
        [HttpPost]
        public ActionResult DeleteProduct(int Id = 0)
        {
            var product = context.Products.FirstOrDefault(x => x.Id == Id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return RedirectToAction("ShowProducts");
        }
        [HttpPost]
        public ActionResult DeletePicture( int pictureid)
        {
            var picture = context.Pictures.FirstOrDefault(V=>V.Id== pictureid);
            if(picture != null)
            {
                context.Pictures.Remove(picture);
                context.SaveChanges();
            }
            
            
            return RedirectToAction("ShowProducts");
        }
        public ActionResult Categories()
        {
            var model = context.Categories;
            return View(model);
            

        }
        public ActionResult EditCategory(string returnUrl,int Id = 0)
        {
            var model = context.Categories.FirstOrDefault(x=>x.Id == Id);
            model = model == null ? new Category() : model;
            return View(model);
            
        }
        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var cat = context.Categories.FirstOrDefault(x => x.Id == category.Id);
                if (cat == null)
                {
                    context.Categories.Add(category);
                }
                else {
                    cat.Name = category.Name;
                }
                context.SaveChanges();
            }
            return RedirectToAction("Categories");
        }
        public ActionResult DeleteCategory(int Id)
        {
            var category = context.Categories.FirstOrDefault(x => x.Id == Id);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
            return RedirectToAction("Categories");
        }
        public ActionResult AddCategory(string returnUrl)
        {
            return RedirectToAction("EditCategory", new { returnUrl });

        }
         
    }
}