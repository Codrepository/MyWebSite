using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopingWebSite.Models;
using System.Web.Helpers;

namespace ShopingWebSite.Controllers
{
    public class PictureController : Controller
    {
        DatabaseContext context = new DatabaseContext();
        // GET: Picture
        public FileContentResult GetImage(int Id)
        {
            Picture picure = context.Pictures.FirstOrDefault(x => x.Id == Id);
            WebImage image = new WebImage(picure.ImageData);
            image.Resize(150, 150, false);
            if (picure != null)
            {
                return File(image.GetBytes(null), picure.ImageMimeType);
            }
            else return null;

        }
    }
}