using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MasterChef.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly ILogger<HomeController> _logger;
        protected readonly IConfiguration _config;
        protected readonly RestClient _client;

        public ControllerBase(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            _client = new RestClient(_config.GetValue<string>("ApiAddress"));
        }
    }
}
