using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BPMSApi.Model.Authentication;
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
        public List<UpdateBPMSTaskRequest> UpdateBPMSTask([FromBody] UpdateBPMSTaskRequest request)
        {
            var results = BPMSApiInstance.BPMSTaskFunctions.UpdateBPMSTask(request).Result;
            return results ?? new List<UpdateBPMSTaskRequest>(); ;
        }

        [HttpGet("[action]")]
        public List<BPMSTaskData> GetBPMSTasks()
        {
            var results = BPMSApiInstance.BPMSTaskFunctions.GetBPMSTasks().Result?.Data;
            return results ?? new List<BPMSTaskData>();
        }

        [HttpGet("[action]")]
        public List<UpdateBPMSTaskRequest> GetBPMSApprovals()
        {
            var results = BPMSApiInstance.BPMSTaskFunctions.GetBPMSApprovals().Result;
            return results ?? new List<UpdateBPMSTaskRequest>();
        }

        [HttpPost("[action]")]
        public Boolean Register([FromBody] User request)
        {
            return BPMSApiInstance.AuthenticationFunctions.Register(request).Result;
        }

        [HttpPost("[action]")]
        public String Login([FromBody] User request)
        {
            var response = BPMSApiInstance.AuthenticationFunctions.Login(request).Result;
            return JsonConvert.SerializeObject(response);
        }
    }
}
