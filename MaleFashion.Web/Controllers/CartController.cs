using Microsoft.AspNetCore.Mvc;
using MaleFashion.Web.Models;
using MaleFashion.Web.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MaleFashion.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly Data.AppDbContext _context;

        public CartController(Data.AppDbContext context)
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

            var cart = _context.CartItems.Include(c => c.Product).Where(c => c.UserId == userId).ToList();
            return View(cart);
        }

        public IActionResult AddToCart(int id, int quantity = 1)
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
            var cartItems = _context.CartItems.Where(c => c.UserId == userId).ToList();

            if (product != null)
            {
                var item = cartItems.FirstOrDefault(i => i.ProductId == id);
                if (item != null)
                {
                    isDuplicate = true;
                }
                else
                {
                    var newItem = new CartItem { ProductId = id, UserId = userId, Quantity = quantity };
                    _context.CartItems.Add(newItem);
                    _context.SaveChanges();
                    cartItems.Add(newItem);
                }
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { 
                    success = true, 
                    isDuplicate = isDuplicate, 
                    count = cartItems.Sum(x => x.Quantity), 
                    message = isDuplicate ? "Already Added to Cart" : "Added to your Cart" 
                });
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var userId = GetCurrentUserId();
            if (userId == null) return RedirectToAction("Login", "Account");

            var item = _context.CartItems.FirstOrDefault(i => i.ProductId == id && i.UserId == userId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
