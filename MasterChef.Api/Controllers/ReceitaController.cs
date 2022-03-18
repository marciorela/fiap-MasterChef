using MasterChef.Api.DTO.Receita;
using MasterChef.Contracts.Services;
using MasterChef.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MasterChef.Api.Controllers
{
    [ApiController]
    [Route("api/Receitas")]
    public class ReceitaController : ControllerBase
    {
        private readonly IReceitaService _receitaService;

        public ReceitaController(IReceitaService receitaService)
        {
            _receitaService = receitaService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll(string? search)
        {
            var list = await _receitaService.GetAll(search);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var receita = _receitaService.GetById(id);
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
        // TODO: 
        public async Task<IActionResult> New(ReceitaRequest receita)
        {
            var receitaNova = await _receitaService.Add(new Receita(receita.Titulo, receita.Descricao, receita.Ingredientes, receita.ModoDePreparo));

            return Created("", receitaNova);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ReceitaRequest receita)
        {
            await _receitaService.Update(id, new Receita(receita.Titulo, receita.Descricao, receita.Ingredientes, receita.ModoDePreparo));

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
