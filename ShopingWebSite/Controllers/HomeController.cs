using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopingWebSite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ShopingWebSite.Controllers
{
	public class HomeController : Controller
	{
        DatabaseContext context = new DatabaseContext();
		public ActionResult Index()
		{
            return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			

			return View();
		}
        
	}
}