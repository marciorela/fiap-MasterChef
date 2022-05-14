using MasterChef.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;
using System.Net.Http;

namespace MasterChef.Controllers
{
    public class HomeController : ControllerBase
    {
        public HomeController(ILogger<HomeController> logger, IConfiguration config) : base(logger, config)
        {
        }

        public IActionResult Index()
        {

            return RedirectToAction(nameof(ReceitaController.Index), typeof(ReceitaController).ControllerName());

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

    public static class ControllerExtensions
    {
        public static string ControllerName(this Type controller)
        {
            var name = controller.Name;
            return name.EndsWith("Controller") ? name[0..^10] : name;
        }
    }
}