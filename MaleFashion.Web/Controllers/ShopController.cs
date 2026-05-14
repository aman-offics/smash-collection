using Microsoft.AspNetCore.Mvc;
using MaleFashion.Web.Models;
using System.Collections.Generic;
using System.Linq;
using MaleFashion.Web.Services;

namespace MaleFashion.Web.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProductRepository _repository;

        public ShopController(ProductRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(string search, string category, string brand, string size, string color, string tag, string priceRange, string sortOrder, int page = 1)
        {
            decimal? minPrice = null;
            decimal? maxPrice = null;

            if (!string.IsNullOrEmpty(priceRange))
            {
                // Expected format: "₹0.00 - ₹5500.00" or "5000.00+"
                string cleanRange = priceRange.Replace("₹", "").Replace("$", "").Trim();
                if (cleanRange.Contains("+"))
                {
                     var parts = cleanRange.Replace("+", "").Trim();
                     if (decimal.TryParse(parts, out decimal min))
                     {
                         minPrice = min;
                     }
                }
                else if (cleanRange.Contains("-"))
                {
                    var parts = cleanRange.Split('-');
                    if (parts.Length == 2)
                    {
                        if (decimal.TryParse(parts[0].Trim(), out decimal min)) minPrice = min;
                        if (decimal.TryParse(parts[1].Trim(), out decimal max)) maxPrice = max;
                    }
                }
            }

            ViewBag.CurrentSearch = search;
            ViewBag.CurrentCategory = category;
            ViewBag.CurrentBrand = brand;
            ViewBag.CurrentSize = size;
            ViewBag.CurrentColor = color;
            ViewBag.CurrentTag = tag;
            ViewBag.CurrentPriceRange = priceRange;
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentPage = page;

            var (products, totalCount) = _repository.GetProducts(search, category, brand, size, color, tag, minPrice, maxPrice, sortOrder, page);
            
            ViewBag.TotalCount = totalCount;
            ViewBag.PageSize = 9; // Hardcoded for now matches repository
            ViewBag.TotalPages = (int)System.Math.Ceiling((double)totalCount / 9);

            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _repository.GetById(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            
            // Fetch related products (just taking top 4 for now to mimic "Related Products")
            ViewBag.RelatedProducts = _repository.GetAll().Take(4).ToList();
            
            return View(product);
        }
    }
}
