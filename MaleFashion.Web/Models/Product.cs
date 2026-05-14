using System.Collections.Generic;

namespace MaleFashion.Web.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public string? ImageUrl { get; set; }
        public string? Label { get; set; } // "New", "Sale", etc.
        public int Rating { get; set; } // 1-5
        public List<string> Colors { get; set; } = new List<string>();
        public List<string> Sizes { get; set; } = new List<string>();
        public List<string> Tags { get; set; } = new List<string>();
        public string? Category { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
    }
}
