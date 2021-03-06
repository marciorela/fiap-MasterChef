using AutoMapper;
using MasterChef.Contracts.Services;
using MasterChef.Domain.Dto;
using MasterChef.Domain.Entities;
using MasterChef.Extensions;
using MasterChef.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Text;

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
		public async Task<IActionResult> Create()
		{
			var request = new RestRequest("Categorias", Method.Get);
			request = await AddToken(request);

			var response = await _client.GetAsync<IEnumerable<Categoria>>(request);

			if (response is not null)			
				ViewBag.Categorias = response.Select(m => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = m.Id.ToString(), Text = m.Titulo.ToString() }).ToList();

			return View(new ReceitaViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> Create(ReceitaViewModel viewModel, IFormFile FotoName)
		{
			var _mappedReceita = _mapper.Map<ReceitaCreateRequest>(viewModel);
			_mappedReceita.FotoName = FotoName?.FileName;
			_mappedReceita.FotoContent = await FotoName.ToBase64();

			var request = new RestRequest("Receitas", Method.Post).AddJsonBody(_mappedReceita);
			request = await AddToken(request);

			var response = await _client.PostAsync(request);

			if (response.StatusCode == HttpStatusCode.Created && FotoName != null && response != null && response.Content != null)
			{
				var receita = JsonConvert.DeserializeObject<Receita>(response.Content);
				if (receita != null)
				{
					request = new RestRequest("Receitas/UploadFoto/{id}", Method.Post)
						.AddUrlSegment("id", receita.Id)
						.AddHeader("Content-Type", "multipart/form-data")
						.AddFile("fotoStr", await FotoName.GetBytes(), FotoName.FileName);
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
			var requestCategorias = new RestRequest("Categorias", Method.Get);
			requestCategorias = await AddToken(requestCategorias);

			var responseCategorias = await _client.GetAsync<IEnumerable<Categoria>>(requestCategorias);

			if (responseCategorias is not null)
				ViewBag.Categorias = responseCategorias.Select(m => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = m.Id.ToString(), Text = m.Titulo.ToString() }).ToList();

			var request = new RestRequest("Receitas/{id}", Method.Get).AddUrlSegment("id", id);
			request = await AddToken(request);

			var response = await _client.GetAsync(request);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				if (response.Content != null)
				{
					var _mappedReceita = _mapper.Map<ReceitaViewModel>(JsonConvert.DeserializeObject<ReceitaResponse>(response.Content));

					return View(_mappedReceita);
				}
				else
					return NotFound();
			}
			else
				return StatusCode((int)response.StatusCode);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Guid id, ReceitaViewModel model, IFormFile foto)
		{
			var _mappedReceita = _mapper.Map<ReceitaCreateRequest>(model);
			if (foto != null)
			{
				_mappedReceita.FotoName = foto.Name;
				_mappedReceita.FotoContent = await foto.ToBase64();
			}

			var request = new RestRequest("Receitas/{id}", Method.Put).AddUrlSegment("id", id).AddJsonBody(_mappedReceita);			
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