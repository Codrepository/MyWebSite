namespace ShopingWebSite.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ShopingWebSite.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<ShopingWebSite.Models.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ShopingWebSite.Models.DatabaseContext";

        }

        protected override void Seed(ShopingWebSite.Models.DatabaseContext context)

        {
            try
            {
                context.Database.ExecuteSqlCommand("ALTER TABLE dbo.Products ADD CONSTRAINT Products_Category FOREIGN KEY (Category_Id) REFERENCES dbo.Categories(Id) ON UPDATE CASCADE ON DELETE SET NULL");
            }
            catch (Exception)
            { }
            ApplicationDbContext cont = new ApplicationDbContext();
            RoleManager<IdentityRole> rolmanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(cont));
            var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole("Admin");
            rolmanager.Create(role);
            UserManager<ApplicationUser> usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(cont));
            ApplicationUser user = new ApplicationUser { UserName = "omid.safari20@gmail.com" };

            var result = usermanager.Create(user, "123456");
            var rol = rolmanager.FindByName("Admin");
            var us = usermanager.FindByName("omid.safari20@gmail.com");
            usermanager.AddToRole(us.Id, rol.Name);

            var category1 = new Category { Id = 1, Name = "نرم افزار" };
            var category2 = new Category { Id = 2, Name = "سخت افزار" };
            var category3 = new Category { Id = 3, Name = "کتاب ها" };
            var product1 = new Product { Id = 1, Name = "Visual Studio 2017", Description = "جدیدترین ورژن ویژوال استودیو", Price = 200,Quantity = 2, Category = category1 };
            var product2 = new Product { Id = 2, Name = "Visual Studio 2015", Description = "این ورژن خیلی محبوب و پایدار می باشد.", Price = 150, Quantity = 0, Category = category1 };
            var product3 = new Product { Id = 3, Name = "Netbean IDE 8.1", Description = "یکی از ابزار های قدرمند برای نوشتن کدهای جاوا ", Price = 200, Quantity = 2, Category = category1 };
            var product4 = new Product { Id = 4, Name = "wireless Mouse", Description = "موس  بیسیم محبوب ترین موس در سال جاری", Price = 200, Quantity = 2, Category = category2 };
            var product5 = new Product { Id = 5, Name = "keyboard", Description = "ارزان ترین و بهترین کیبورد ", Price = 200, Quantity = 6, Category = category2 };
            var product6 = new Product { Id = 6, Name = "pro asp.net mvc", Description = "بهترین کتاب برای اموزش  mvc", Price = 200, Quantity = 2, Category = category3 };
            context.Categories.AddOrUpdate(category1);
            context.Categories.AddOrUpdate(category2);
            context.Categories.AddOrUpdate(category3);
            context.Products.AddOrUpdate(product1);
            context.Products.AddOrUpdate(product2);
            context.Products.AddOrUpdate(product3);
            context.Products.AddOrUpdate(product4);
            context.Products.AddOrUpdate(product5);
            context.Products.AddOrUpdate(product6);
            context.SaveChanges();
        }
        
    }
}
