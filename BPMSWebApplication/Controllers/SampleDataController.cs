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

        [HttpPut("[action]")]
        public List<BPMSTaskData> UpdateBPMSTask([FromBody] UpdateBPMSTaskRequest request)
        {
            var results = BPMSApiInstance.BPMSTaskFunctions.UpdateBPMSTask(request).Result?.Data;
            return results ?? new List<BPMSTaskData>(); ;
        }

        [HttpGet("[action]")]
        public List<BPMSTaskData> GetBPMSTasks()
        {
            var results = BPMSApiInstance.BPMSTaskFunctions.GetBPMSTasks().Result?.Data;
            return results ?? new List<BPMSTaskData>();
        }
    }
}
