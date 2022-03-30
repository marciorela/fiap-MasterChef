using MasterChef.Domain.Dto;
using MasterChef.Domain.Entities;
using MasterChef.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MasterChef.Extensions;

namespace MasterChef.Controllers
{
    public class ReceitaController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ReceitaController(
            ILogger<HomeController> logger,
            IConfiguration config,
            IWebHostEnvironment env
            ) : base(logger, config)
        {
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            //HttpPostedFileBase

            var request = new RestRequest("Receitas", Method.Get)
                .AddQueryParameter("search", search);

            var response = await _client.GetAsync<IEnumerable<ReceitaStdResponse>>(request);

            //ViewData["TestApi"] = response;

            return View(response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ReceitaVM());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReceitaVM model, IFormFile foto)
        {
            var request = new RestRequest("Receitas", Method.Post)
                .AddJsonBody(new ReceitaCreateRequest()
                {
                    Titulo = model.Titulo,
                    Descricao = model.Descricao,
                    Ingredientes = model.Ingredientes,
                    ModoDePreparo = model.ModoDePreparo,
                    FotoName = foto?.FileName,
                    FotoContent = await foto.ToBase64()
                });
            var response = await _client.PostAsync(request);

            if (response.StatusCode == HttpStatusCode.Created && foto != null && response != null && response.Content != null)
            {
                var receita = JsonConvert.DeserializeObject<Receita>(response.Content);
                if (receita != null)
                {
                    request = new RestRequest("Receitas/UploadFoto/{id}", Method.Post)
                        .AddUrlSegment("id", receita.Id)
                        .AddHeader("Content-Type", "multipart/form-data")
                        //.AddFile("fotoStr", System.IO.File.ReadAllBytes(fileName), fileName);
                        .AddFile("fotoStr", await foto.GetBytes(), foto.FileName);
                    request.AlwaysMultipartFormData = true;
                    response = await _client.PostAsync(request);
                }
            }

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
        public async Task<IActionResult> Edit(Guid id, Receita model, IFormFile foto)
        {
            var request = new RestRequest("Receitas/{id}", Method.Put)
                .AddUrlSegment("id", id)
                .AddJsonBody(new ReceitaCreateRequest() {
                    Descricao = model.Descricao,
                    Ingredientes = model.Ingredientes,
                    ModoDePreparo = model.ModoDePreparo,
                    Titulo = model.Titulo,
                    FotoName = foto?.FileName,
                    FotoContent = await foto.ToBase64()
                });
            var response = await _client.PutAsync(request);

            return RedirectToAction(nameof(ReceitaController.Index));
        }

        private static bool ValidaImagem(IFormFile foto)
        {
            return foto.ContentType switch
            {
                "image/jpeg" => true,
                "image/bmp" => true,
                "image/gif" => true,
                "image/png" => true,
                _ => false,
            };
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