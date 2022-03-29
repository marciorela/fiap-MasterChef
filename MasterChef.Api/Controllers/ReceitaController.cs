using System.Reflection;
using MasterChef.Contracts.Services;
using MasterChef.Domain.Dto;
using MasterChef.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MasterChef.Api.Controllers
{
    [ApiController]
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
            } else
            {
                return Ok(receita);
            }
        }

        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        // TODO: DOCUMENTACAO
        public async Task<IActionResult> New(ReceitaCreateRequest receita)
        {
            var receitaNova = await _receitaService.Add(new Receita(receita.Titulo, receita.Descricao, receita.Ingredientes, receita.ModoDePreparo));

            return Created("", receitaNova);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ReceitaCreateRequest receita)
        {
            await _receitaService.Update(new Receita(
                receita.Titulo, 
                receita.Descricao, 
                receita.Ingredientes, 
                receita.ModoDePreparo) {
                Id = id,
                Foto = receita.Foto
            });

            return Ok();
        }

        [HttpPost]
        [Route("UploadFoto/{id}")]
        public async Task<IActionResult> Upload([FromRoute] Guid id, IFormFile fotoStr)
        {
            try
            {
                await _fotoService.Save(fotoStr, id);
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _receitaService.Delete(id);

            return Ok();
        }
    }
}
