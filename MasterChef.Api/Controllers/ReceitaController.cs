using MasterChef.Contracts.Services;
using MasterChef.Domain.Dto;
using MasterChef.Domain.Entities;
using MasterChef.Domain.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterChef.Api.Controllers
{
	[ApiController]
	[Authorize]
	[Route("api/Receitas")]
	public class ReceitaController : ControllerBase
	{
		private readonly IReceitaService _receitaService;
		private readonly IFotoService _fotoService;

		public ReceitaController(
			IReceitaService receitaService,
			IFotoService fotoService
			)
		{
			_receitaService = receitaService;
			_fotoService = fotoService;
		}

		[HttpGet()]
		public async Task<IActionResult> GetAll(string? search)
		{
			var list = await _receitaService.GetAll(search);
			return Ok(list);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var receita = await _receitaService.GetById(id);

			if (receita == null)
			{
				return NotFound();
			}
			else
			{
				ReceitaResponse response = new ReceitaResponse()
				{
					CategoriaId = receita.CategoriaId,
					DataCadastro = receita.DataCadastro,
					Descricao = receita.Descricao,
					Foto = receita.Foto,
					FotoContent = _fotoService.Load(receita).ContentBase64,
					Id = receita.Id,
					Ingredientes = receita.Ingredientes,
					ModoDePreparo = receita.ModoDePreparo,
					Tags = receita.Tags,
					Titulo = receita.Titulo
				};

				return Ok(response);
			}
		}

		[HttpPost]
		//[ProducesResponseType(StatusCodes.Status201Created)]
		//[ProducesResponseType(StatusCodes.Status400BadRequest)]
		// TODO: DOCUMENTACAO
		public async Task<IActionResult> New(ReceitaCreateRequest receita)
		{
			var receitaNova = await _receitaService.Add(
				new Receita(receita.Titulo, receita.Descricao, receita.Ingredientes, receita.ModoDePreparo, receita.Tags, receita.CategoriaId),
				new FotoInfo(receita.FotoName, receita.FotoContent)
			);

			return Created("", receitaNova);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(Guid id, ReceitaCreateRequest receita)
		{
			await _receitaService.Update(new Receita(
				receita.Titulo,
				receita.Descricao,
				receita.Ingredientes,
				receita.ModoDePreparo,
				receita.Tags,
				receita.CategoriaId)
			{
				Id = id
			}, new FotoInfo(receita.FotoName, receita.FotoContent));

			return Ok();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _receitaService.Delete(id);

			return Ok();
		}
	}
}
