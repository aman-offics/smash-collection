using Microsoft.AspNetCore.Mvc;
using MaleFashion.Web.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MaleFashion.Web.Controllers
{
    public class WishlistController : Controller
    {
        private readonly Data.AppDbContext _context;

        public WishlistController(Data.AppDbContext context)
        {
            _context = context;
        }

        private string? GetCurrentUserId()
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null) return null;
            return _context.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower())?.Id;
        }

        public IActionResult Index()
        {
            var userId = GetCurrentUserId();
            if (userId == null) return RedirectToAction("Login", "Account");

            var wishlist = _context.WishlistItems.Include(w => w.Product).Where(w => w.UserId == userId).Select(w => w.Product).ToList();
            return View(wishlist);
        }

        public IActionResult AddToWishlist(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return Json(new { success = false, message = "Please sign in to add items", needsLogin = true });
                }
                return RedirectToAction("Login", "Account");
            }

            var product = _context.Products.Find(id);
            bool isDuplicate = false;
            var wishlistItems = _context.WishlistItems.Where(w => w.UserId == userId).ToList();

            if (product != null)
            {
                var item = wishlistItems.FirstOrDefault(i => i.ProductId == id);
                if (item != null)
                {
                    isDuplicate = true;
                }
                else
                {
                    var newItem = new WishlistItem { ProductId = id, UserId = userId };
                    _context.WishlistItems.Add(newItem);
                    _context.SaveChanges();
                    wishlistItems.Add(newItem);
                }
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { 
                    success = true, 
                    isDuplicate = isDuplicate, 
                    count = wishlistItems.Count, 
                    message = isDuplicate ? "Already Added to Wishlist" : "Added to your Wishlist" 
                });
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromWishlist(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return RedirectToAction("Login", "Account");

            var item = _context.WishlistItems.FirstOrDefault(i => i.ProductId == id && i.UserId == userId);
            if (item != null)
            {
                _context.WishlistItems.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
