using MasterChef.Domain.Dto;
using MasterChef.Domain.Entities;
using MasterChef.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Controllers
{
    public class ReceitaController : ControllerBase
    {
        public ReceitaController(
            ILogger<HomeController> logger, 
            IConfiguration config
            ) : base(logger, config)
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

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ReceitaVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReceitaVM model)
        {
            var request = new RestRequest("Receitas", Method.Post)
                .AddJsonBody(new ReceitaCreateRequest()
                {
                    Titulo = model.Titulo,
                    Descricao = model.Descricao,
                    Ingredientes = model.Ingredientes,
                    ModoDePreparo = model.ModoDePreparo,
                });
            var response = await _client.PostAsync(request);            

            return RedirectToAction(nameof(ReceitaController.Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var request = new RestRequest("Receitas/{id}", Method.Get)
                .AddUrlSegment("id", id);
            var response = await _client.GetAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Content != null)
                {
                    return View(JsonConvert.DeserializeObject<Receita>(response.Content));
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, Receita model)
        {
            var request = new RestRequest("Receitas/{id}", Method.Put)
                .AddUrlSegment("id", id)
                .AddJsonBody(model);
            var response = await _client.PutAsync(request);

            return RedirectToAction(nameof(ReceitaController.Index));
        }
 
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new RestRequest("Receitas/{id}", Method.Delete)
                .AddUrlSegment("id", id);
            var response = await _client.DeleteAsync(request);

            return StatusCode((int)response.StatusCode);
        }
    }
}