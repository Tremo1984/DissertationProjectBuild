using System.Diagnostics;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IServiceWrapper _serviceWrapper;

    public HomeController(ILogger<HomeController> logger, IServiceWrapper serviceWrapper)
    {
        _logger = logger;
        _serviceWrapper = serviceWrapper;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(string reg) 
    {
        _serviceWrapper.MotHistoryApiService.GetVehicleData(reg);

        return View();
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