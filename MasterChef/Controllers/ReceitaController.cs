using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Controllers
{
    public class ReceitaController : ControllerBase
    {
        public ReceitaController(ILogger<HomeController> logger, IConfiguration config) : base(logger, config)
        {
        }

        [HttpGet]
        public IActionResult Index(string search)
        {
            return View();
        }

    }
}
