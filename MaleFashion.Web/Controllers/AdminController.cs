using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MaleFashion.Web.Models;
using MaleFashion.Web.Data;
using System.Linq;

namespace MaleFashion.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        private bool IsAdmin()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username)) return false;

            var user = _context.Users.FirstOrDefault(u => u.Username.ToLower() == username.ToLower());
            return user != null && user.IsAdmin;
        }

        public IActionResult Index()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            ViewBag.TotalUsers = _context.Users.Count();
            ViewBag.TotalProducts = _context.Products.Count();
            return View();
        }

        // --- USERS MANAGEMENT ---

        public IActionResult Users()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public IActionResult ToggleUserStatus(string id)
        {
            if (!IsAdmin()) return Json(new { success = false, message = "Unauthorized" });

            var user = _context.Users.Find(id);
            if (user != null)
            {
                if (user.Username.ToLower() == "admin")
                {
                    return Json(new { success = false, message = "Cannot deactivate super admin" });
                }

                user.IsActive = !user.IsActive;
                _context.SaveChanges();
                return Json(new { success = true, isActive = user.IsActive, message = "User status updated" });
            }
            return Json(new { success = false, message = "User not found" });
        }

        public IActionResult EditUser(string id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(User updatedUser)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            var user = _context.Users.Find(updatedUser.Id);
            if (user != null)
            {
                user.Email = updatedUser.Email;
                user.PhoneNumber = updatedUser.PhoneNumber;
                if (user.Username.ToLower() != "admin")
                {
                    user.IsAdmin = updatedUser.IsAdmin;
                }
                _context.SaveChanges();
                return RedirectToAction("Users");
            }
            return View(updatedUser);
        }

        // --- PRODUCTS MANAGEMENT ---

        public IActionResult Products()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult AddProduct()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            return View(new Product());
        }

        [HttpPost]
        public IActionResult AddProduct(Product product, string colorsInput, string sizesInput, string tagsInput)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            
            product.Colors = string.IsNullOrEmpty(colorsInput) ? new System.Collections.Generic.List<string>() : colorsInput.Split(',').Select(s => s.Trim()).ToList();
            product.Sizes = string.IsNullOrEmpty(sizesInput) ? new System.Collections.Generic.List<string>() : sizesInput.Split(',').Select(s => s.Trim()).ToList();
            product.Tags = string.IsNullOrEmpty(tagsInput) ? new System.Collections.Generic.List<string>() : tagsInput.Split(',').Select(s => s.Trim()).ToList();
            
            product.Label ??= "";
            product.Description ??= "";
            product.Category ??= "";
            product.Brand ??= "";

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Product Added Successfully!";
                return RedirectToAction("Products");
            }
            return View(product);
        }

        public IActionResult EditProduct(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            
            ViewBag.ColorsInput = string.Join(", ", product.Colors ?? new System.Collections.Generic.List<string>());
            ViewBag.SizesInput = string.Join(", ", product.Sizes ?? new System.Collections.Generic.List<string>());
            ViewBag.TagsInput = string.Join(", ", product.Tags ?? new System.Collections.Generic.List<string>());
            
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(Product product, string colorsInput, string sizesInput, string tagsInput)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var existing = _context.Products.Find(product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.OldPrice = product.OldPrice;
                existing.ImageUrl = product.ImageUrl;
                existing.Label = product.Label ?? "";
                existing.Rating = product.Rating;
                existing.Category = product.Category ?? "";
                existing.Brand = product.Brand ?? "";
                existing.Description = product.Description ?? "";
                existing.Colors = string.IsNullOrEmpty(colorsInput) ? new System.Collections.Generic.List<string>() : colorsInput.Split(',').Select(s => s.Trim()).ToList();
                existing.Sizes = string.IsNullOrEmpty(sizesInput) ? new System.Collections.Generic.List<string>() : sizesInput.Split(',').Select(s => s.Trim()).ToList();
                existing.Tags = string.IsNullOrEmpty(tagsInput) ? new System.Collections.Generic.List<string>() : tagsInput.Split(',').Select(s => s.Trim()).ToList();

                _context.SaveChanges();
                TempData["SuccessMessage"] = "Product Updated Successfully!";
                return RedirectToAction("Products");
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            if (!IsAdmin()) return Json(new { success = false, message = "Unauthorized" });

            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Json(new { success = true, message = "Product deleted" });
            }
            return Json(new { success = false, message = "Product not found" });
        }

        // --- CONTACT MESSAGES ---

        public IActionResult Messages()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            var messages = _context.ContactMessages.OrderByDescending(m => m.SentAt).ToList();
            return View(messages);
        }
    }
}
