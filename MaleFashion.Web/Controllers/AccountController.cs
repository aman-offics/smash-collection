using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MaleFashion.Web.Models;
using MaleFashion.Web.Services;

namespace MaleFashion.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserRepository _userRepository;

        public AccountController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("Profile");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (_userRepository.ValidateUser(username, password))
            {
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("Profile");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
            {
                ViewBag.Error = "Username and Password are required.";
                return View(user);
            }

            if (_userRepository.GetUserByUsername(user.Username) != null)
            {
                ViewBag.Error = "Username already exists.";
                return View(user);
            }

            _userRepository.AddUser(user);
            TempData["SuccessMessage"] = "Registration successful! Please sign in.";
            
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null)
            {
                return RedirectToAction("Login");
            }

            var user = _userRepository.GetUserByUsername(username);
            if (user == null)
            {
                return RedirectToAction("Logout"); // Just in case session is out of sync with memory
            }

            return View(user);
        }
    }
}
