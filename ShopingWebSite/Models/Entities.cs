using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
namespace ShopingWebSite.Models
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        public string ImageMimeType { get; set; }
        public byte[] ImageData { get; set; }
        public virtual Product Product { get; set; }
    }
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<Product> Products { get; set; }

    }
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public virtual Category Category { get; set; }
        public virtual IList<Picture> Pictures { get; set; }

    }
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("ShopingWebSiteContextdb") { }
        static DatabaseContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //set on cascade delete for picture
            modelBuilder.Entity<Picture>()
                .HasOptional<Product>(x => x.Product)
                .WithMany().WillCascadeOnDelete(true);
            



        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Picture> Pictures { get; set; }

    }

    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); } }
    }
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
    public class Cart
    {
        private List<CartLine> lineCollection = new List<Models.CartLine>();
        public void AddToLine(Product p,int quantity)
        {
            var cartline = lineCollection.Where(x => x.Product.Id == p.Id).FirstOrDefault();
            if (cartline == null)
            {
                lineCollection.Add(new CartLine { Product = p, Quantity = quantity });
            }
            else
            {
                cartline.Quantity++;
            }
        }
        public void RemoveFromLine(Product p)
        {
            lineCollection.RemoveAll(x => x.Product.Id == p.Id);
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(x => x.Product.Price * x.Quantity);
        }
        public void Clear()
        {
            lineCollection = new List<CartLine>();
        }
        public IEnumerable<CartLine> Lines { get { return lineCollection; } }
    }
    public class ShipingDetail
    {
        [Required(ErrorMessage ="استان محل زندی را وارد کنید")]
        [Display(Name = "استان")]
        public string State{ get; set; }
        [Display(Name ="شهر")]
        public string City { get; set; }
        [Display(Name ="کد پستی")]
        public string Zip { get; set; }
        [Required(ErrorMessage ="آدرس خود را وارد کنید")]
        [Display(Name ="آدرس 1")]
        public string Address1 { get; set; }
        [Display(Name ="آدرس 2")]
        public string Address2 { get; set; }
    }
    public enum Tables
    {
        Product = 0, Category, Picture
    }
}