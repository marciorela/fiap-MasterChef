using MasterChef.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Controllers
{
    public class ReceitaController : ControllerBase
    {
        public ReceitaController(ILogger<HomeController> logger, IConfiguration config) : base(logger, config)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {

            var request = new RestRequest("Receitas", Method.Get)
                .AddQueryParameter("search", search);

            var response = await _client.GetAsync<IEnumerable<Receita>>(request);
            
            //ViewData["TestApi"] = response;

            return View(response);
        }
    }
}