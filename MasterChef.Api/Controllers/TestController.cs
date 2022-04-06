using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MasterChef.Api.Controllers
{
    [ApiController]
    //[Authorize]
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
