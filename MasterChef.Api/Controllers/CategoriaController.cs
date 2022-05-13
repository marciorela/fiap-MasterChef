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

        [HttpGet]
        public async Task<List<Categoria>> GetAll([FromServices] CategoriaRepository categoriaRepository)
        {
            return await categoriaRepository.GetAll();
        }

    }
}
