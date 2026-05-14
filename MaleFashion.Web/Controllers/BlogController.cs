using Microsoft.AspNetCore.Mvc;
using MaleFashion.Web.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace MaleFashion.Web.Controllers
{
    public class BlogController : Controller
    {
        private static List<BlogPost> _posts = new List<BlogPost>
        {
            new BlogPost
            {
                Id = 1,
                Title = "What Curling Irons Are The Best Ones",
                Date = new DateTime(2020, 2, 16),
                Author = "Deercreative",
                ImageUrl = "/img/blog/blog-1.jpg",
                Excerpt = "Read More",
                Content = "Hydroderm is the highly desired anti-aging cream on the block.",
                CommentsCount = 8
            },
            new BlogPost
            {
                Id = 2,
                Title = "Eternity Bands Do Last Forever",
                Date = new DateTime(2020, 2, 21),
                Author = "Deercreative",
                ImageUrl = "/img/blog/blog-2.jpg",
                Excerpt = "Read More",
                Content = "Hydroderm is the highly desired anti-aging cream on the block.",
                CommentsCount = 0
            },
            new BlogPost
            {
                Id = 3,
                Title = "The Health Benefits Of Sunglasses",
                Date = new DateTime(2020, 2, 28),
                Author = "Deercreative",
                ImageUrl = "/img/blog/blog-3.jpg",
                Excerpt = "Read More",
                Content = "Hydroderm is the highly desired anti-aging cream on the block.",
                CommentsCount = 0
            },
            new BlogPost
            {
                Id = 4,
                Title = "Aiming For Higher The Mastopexy",
                Date = new DateTime(2020, 2, 16),
                Author = "Deercreative",
                ImageUrl = "/img/blog/blog-4.jpg",
                Excerpt = "Read More",
                Content = "Hydroderm is the highly desired anti-aging cream on the block.",
                CommentsCount = 0
            },
            new BlogPost
            {
                Id = 5,
                Title = "Wedding Rings A Gift For A Lifetime",
                Date = new DateTime(2020, 2, 21),
                Author = "Deercreative",
                ImageUrl = "/img/blog/blog-5.jpg",
                Excerpt = "Read More",
                Content = "Hydroderm is the highly desired anti-aging cream on the block.",
                CommentsCount = 0
            },
            new BlogPost
            {
                Id = 6,
                Title = "The Different Methods Of Hair Removal",
                Date = new DateTime(2020, 2, 28),
                Author = "Deercreative",
                ImageUrl = "/img/blog/blog-6.jpg",
                Excerpt = "Read More",
                Content = "Hydroderm is the highly desired anti-aging cream on the block.",
                CommentsCount = 0
            },
            new BlogPost
            {
                Id = 7,
                Title = "Hoop Earrings A Style From History",
                Date = new DateTime(2020, 2, 16),
                Author = "Deercreative",
                ImageUrl = "/img/blog/blog-7.jpg",
                Excerpt = "Read More",
                Content = "Hydroderm is the highly desired anti-aging cream on the block.",
                CommentsCount = 0
            },
            new BlogPost
            {
                Id = 8,
                Title = "Lasik Eye Surgery Are You Ready",
                Date = new DateTime(2020, 2, 21),
                Author = "Deercreative",
                ImageUrl = "/img/blog/blog-8.jpg",
                Excerpt = "Read More",
                Content = "Hydroderm is the highly desired anti-aging cream on the block.",
                CommentsCount = 0
            },
            new BlogPost
            {
                Id = 9,
                Title = "Lasik Eye Surgery Are You Ready",
                Date = new DateTime(2020, 2, 28),
                Author = "Deercreative",
                ImageUrl = "/img/blog/blog-9.jpg",
                Excerpt = "Read More",
                Content = "Hydroderm is the highly desired anti-aging cream on the block.",
                CommentsCount = 0
            }
        };

        public IActionResult Index()
        {
            return View(_posts);
        }

        public IActionResult Details(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return RedirectToAction("Index");
            }
            return View(post);
        }
    }
}
