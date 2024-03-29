﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace JobsApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private static readonly HttpClient Client = new HttpClient();

        private static readonly string BaseUrl = "https://remotive.io/api/remote-jobs";

        public JobsController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var jobs = await GetData($"{BaseUrl}{HttpContext.Request.QueryString}");

            return Ok(jobs);
        }

        private static async Task<Jobs> GetData(string url)
        {
            var data = Client.GetStreamAsync(url);
            return await JsonSerializer.DeserializeAsync<Jobs>(await data);
        }
    }

    public class Jobs
    {
        public List<Job> jobs { get; set; }
    }

    [DataContract(Namespace = "")]
    public class Job
    {
        public int id { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string url { get; set; }

        public string company_name { get; set; }

        public string candidate_required_location { get; set; }
    }
}
