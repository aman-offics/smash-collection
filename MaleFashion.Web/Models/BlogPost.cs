using System;
using System.Collections.Generic;

namespace MaleFashion.Web.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Author { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Excerpt { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int CommentsCount { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
