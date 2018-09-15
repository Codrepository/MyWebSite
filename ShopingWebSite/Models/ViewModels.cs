using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web.Mvc;

namespace ShopingWebSite.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PageInfo { get; set; }
        public Category Category { get; set; }
    }
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
    public class AdminProductViewModel
    {
        public Product Product { get; set; }
        public Category Category { get; set; }
    }
    public class AdminEditProductViewModel
    {
        public IEnumerable<SelectListItem> List { get; set; }
        public Product Product { get; set;}
        public string ReturnUrl { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
    }
    public class ProductSummaryViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Picture> Pictures { get; set; }
        
    }
}