using MasterChef.Domain.Dto;
using MasterChef.Domain.Entities;
using MasterChef.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Text;
using MasterChef.Extensions;
using AutoMapper;
using MasterChef.Contracts.Services;

namespace MasterChef.Controllers
{
    public class ReceitaController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ITokenService _tokenService;
        public readonly IMapper _mapper;

        public ReceitaController(
            ILogger<HomeController> logger,
            IConfiguration config,
            IWebHostEnvironment env,
            IMapper mapper,
            ITokenService tokenService
            ) : base(logger, config)
        {
            _env = env;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            _logger.LogInformation("Busca: {search}", search);

            var request = new RestRequest("Receitas", Method.Get)
                .AddQueryParameter("search", search);
            request = await AddToken(request);
            
            var response = await _client.GetAsync<IEnumerable<ReceitaStdResponse>>(request);

            return View(response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateReceitaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReceitaViewModel viewModel, IFormFile foto)
        {
        //    var request = new RestRequest("Receitas", Method.Post)
        //        .AddJsonBody(new ReceitaCreateRequest()
        //        {
        //            Titulo = model.Titulo,
        //            Descricao = model.Descricao,
        //            Ingredientes = model.Ingredientes,
        //            ModoDePreparo = model.ModoDePreparo,
        //            FotoName = foto?.FileName,
        //            FotoContent = await foto.ToBase64()
        //        });
            
            var _mappedReceita = _mapper.Map<ReceitaCreateRequest>(viewModel);
            _mappedReceita.FotoName = foto?.FileName;
            _mappedReceita.FotoName = await foto.ToBase64();

            var request = new RestRequest("Receitas", Method.Post).AddJsonBody(_mappedReceita);
            request = await AddToken(request);

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
                    request = await AddToken(request);
                    response = await _client.PostAsync(request);
                }
            }

            return RedirectToAction(nameof(ReceitaController.Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var request = new RestRequest("Receitas/{id}", Method.Get).AddUrlSegment("id", id);
            request = await AddToken(request);

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
            request = await AddToken(request);
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
            var request = new RestRequest("Receitas/{id}", Method.Delete).AddUrlSegment("id", id);
            request = await AddToken(request);

            var response = await _client.DeleteAsync(request);

            return StatusCode((int)response.StatusCode);
        }

        private async Task<string> GerarTokenAsync()
        {
            return await _tokenService.ObterTokenAsync(_config, _client);
        }
        private async Task<RestRequest> AddToken(RestRequest request)
        {
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + await GerarTokenAsync());
            return request;
        }
    }

}