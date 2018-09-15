using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopingWebSite.Models;
namespace ShopingWebSite.DataAccessClasses
{
    public static class Database
    {
        public static void SaveProduct(this DatabaseContext context,Product product,string categoryName)
        {
            if (product == null) return;
            var category = context.Categories.FirstOrDefault(x => x.Name == categoryName);
            //execute when we want to save new product
            if (product.Id == 0)
            {
                product.Category = category;
                context.Products.Add(product);

            }
            //execute when whe want to edit product
            else {
                var p = context.Products.FirstOrDefault(x => x.Id == product.Id);
                p.Name = product.Name;
                p.Description = product.Description;
                p.Quantity = product.Quantity;
                p.Price = product.Price;
                p.Category = category;
            }
            context.SaveChanges();

        }
        public static void SavePicture(this DatabaseContext context, Picture picture, Product product)
        {
            var prod = context.Products.FirstOrDefault(x => x.Id == product.Id);
            picture.Product = prod;
            context.Pictures.Add(picture);
            context.SaveChanges();
        }
    }
}