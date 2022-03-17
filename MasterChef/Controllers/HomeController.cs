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

        public async Task<IActionResult> Index()
        {

            var request = new RestRequest("test", Method.Get);
            var response = await _client.GetAsync<string>(request);

            ViewData["TestApi"] = response;
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
}