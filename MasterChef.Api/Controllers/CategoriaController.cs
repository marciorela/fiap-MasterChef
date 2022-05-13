using MasterChef.Contracts.Data;
using MasterChef.Data.Repositories;
using MasterChef.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Categorias")]

    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }


        [HttpGet]
        public async Task<List<Categoria>> GetAll()
        {
            return await _categoriaRepository.GetAll();
        }

    }
}
