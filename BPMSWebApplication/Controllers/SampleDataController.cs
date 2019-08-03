using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BPMSApi.Model.BPMSTask;
using BPMSWebApplication.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BPMSWebApplication.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : BaseController
    {
        public SampleDataController(IBPMSSettings settings) : base(settings)
        {
        }

        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpPut("[action]")]
        public GetBPMSTaskResponse UpdateBPMSTask([FromBody] UpdateBPMSTaskRequest request)
        {
            //var id = Request.Form["id"];
            //var request = JsonConvert.DeserializeObject<UpdateBPMSTaskRequest>(Request.Form["request"]);
            return BPMSApiInstance.BPMSTaskFunctions.UpdateBPMSTask(request).Result;
        }

        [HttpGet("[action]")]
        public List<BPMSTaskData> WeatherForecasts()
        {
            return BPMSApiInstance.BPMSTaskFunctions.GetBPMSTasks().Result.Data;
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
