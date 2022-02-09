using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    /// <summary>
    /// Readiness probe.  Success (200 OK) if API Service is responding
    /// </summary>
    [ApiController]
    [Route("readiness")]
    public class Readiness : Controller
    {
        private readonly IConfiguration _config;

        public Readiness(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var client = new HttpClient();
            string url = "http://" + _config["ApiHost"] + "/health";
            try
            {
                _ = await client.GetStringAsync(url);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
