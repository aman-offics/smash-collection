using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using MaleFashion.Web.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MaleFashion.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly Data.AppDbContext _context;

        public CheckoutController(Data.AppDbContext context)
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

        [HttpPost]
        public IActionResult PlaceOrder()
        {
            // For now, return JSON so AJAX can show a popup without a full postback
            return Json(new { success = true, message = "Order Booked Successfully!" });
        }
    }
}
