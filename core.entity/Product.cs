using System;
using System.Collections.Generic;

namespace core.entity
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public double? Price { get; set; }
        public double? Rate { get; set; }
        public int TotalComment { get; set; }
        public int TotalSale { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public DateTime DateAdded { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }
    }
}