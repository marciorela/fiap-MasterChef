using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class TestController : ControllerBase
    {

        [HttpGet("Test")]
        public async Task<string> Test()
        {
            return await Task.Run(() => "Teste Ok");
        }

    }
}
