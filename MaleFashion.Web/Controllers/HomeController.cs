using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MaleFashion.Web.Models;

namespace MaleFashion.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MaleFashion.Web.Services.ProductRepository _productRepository;

    public HomeController(ILogger<HomeController> logger, MaleFashion.Web.Services.ProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
    }

    public IActionResult Index()
    {
        // Get some products for the "Product Section"
        var products = _productRepository.GetAll().Take(8).ToList();
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SubmitContactMessage(string name, string email, string message)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(message))
        {
            return Json(new { success = false, message = "Please fill in all fields." });
        }

        var dbContext = HttpContext.RequestServices.GetService<MaleFashion.Web.Data.AppDbContext>();
        if (dbContext != null)
        {
            var msg = new ContactMessage
            {
                Name = name,
                Email = email,
                Message = message,
                SentAt = System.DateTime.UtcNow
            };
            dbContext.ContactMessages.Add(msg);
            dbContext.SaveChanges();
            
            return Json(new { success = true, message = "Message sent successfully! We will get back to you soon." });
        }

        return Json(new { success = false, message = "An error occurred. Please try again later." });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
