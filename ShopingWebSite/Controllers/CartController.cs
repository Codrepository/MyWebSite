using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopingWebSite.Models;

namespace ShopingWebSite.Controllers
{
    public class CartController : Controller
    {
        DatabaseContext context = new DatabaseContext();
        // GET: Cart
        public RedirectToRouteResult AddToCart(int Id,string returnUrl)
        {
            var product = context.Products.FirstOrDefault(x => x.Id == Id);
            if (product != null)
            {
                getCart().AddToLine(product, 1);
            }
            return RedirectToRoute(new { returnUrl });
        }
        public RedirectToRouteResult RemoveFromCart(int Id, string returnUrl)
        {
            var product = context.Products.FirstOrDefault(x => x.Id == Id);
            if(product != null)
            {
                getCart().RemoveFromLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public ActionResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = getCart(), ReturnUrl = returnUrl });
        }
        public PartialViewResult Summary()
        {
            return PartialView(getCart());
        }
        public ViewResult CheckOut()
        {
            return View(new ShipingDetail());
        }
        [HttpPost]
        [Authorize]
        public ViewResult CheckOut(ShipingDetail shipingdetail)
        {
            if (getCart().Lines.Count() == 0)
            {
                ModelState.AddModelError("", "سبد خرید شما خالی می باشد");
            }
            //we should do something later to complete real shoping...
            if (ModelState.IsValid)
            {
                foreach (var item in getCart().Lines)
                {
                    var product = context.Products.Where(x => x.Id == item.Product.Id).FirstOrDefault();
                    if (product != null)
                    {
                        product.Quantity -= item.Quantity;

                    }
                }
                context.SaveChanges();
                //clear the Cart and the customer can start new shoping
                getCart().Clear();
                return View("Completed");
            }
            else return View(shipingdetail);
        }
        private Cart getCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}