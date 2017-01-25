﻿namespace Widgets.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductType Type { get; set; }
        public decimal Price { get; set; }
    }
}