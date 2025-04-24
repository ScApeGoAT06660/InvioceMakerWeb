using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InvoiceMaker.MVC.Models;

namespace InvoiceMaker.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult About()
    {
        About about = new About
        {
            Name = "Julita",
            Description = "Dupadupadua,"
        };
    
        return View(about);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
