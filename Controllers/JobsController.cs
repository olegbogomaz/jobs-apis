using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JobsApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        public JobsController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var baseUrl = "https://jobs.github.com/positions.json";

            var queryString = HttpContext.Request.QueryString;

            var jobs = await GetData($"{baseUrl}{queryString}");

            return Ok(jobs);
        }

        private static async Task<List<Job>> GetData(string url)
        {
            var data = client.GetStreamAsync(url);
            return await JsonSerializer.DeserializeAsync<List<Job>>(await data);
        }
    }

    public class Job 
    {
        public string title { get; set; }

        public string description { get; set; }

        public string url { get; set; }

        public string company { get; set; }

        public string location { get; set; }

        public string company_logo { get; set; }
    }
}
