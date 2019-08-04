using BPMSApi.Model.BPMSTask;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMSApi.Repositories
{
    public class BPMSTaskFunctions : BaseRepository
    {
        public BPMSTaskFunctions(BPMSApiInstance instance) : base(instance)
        {
        }

        public async Task<GetBPMSTaskResponse> GetBPMSTasks()
        {
            return await Get<GetBPMSTaskResponse>("Tasks");
        }

        public async Task<List<UpdateBPMSTaskRequest>> GetBPMSApprovals()
        {
            return await Get<List<UpdateBPMSTaskRequest>>("Approvals");
        }

        public async Task<List<UpdateBPMSTaskRequest>> UpdateBPMSTask(UpdateBPMSTaskRequest request)
        {
            // Below would be used with an actuall API.
            // var result = await Put<GetBPMSTaskResponse>($"tasks/{request.Id.ToString()}", request);
            var tasks = await GetBPMSTasks();
            var result = await Put<List<UpdateBPMSTaskRequest>>("Approvals", request);
            result.Add(request);
            if(tasks.Data.Any(c => c.Id == request.Id))
            {
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + $"\\Approvals.json", JsonConvert.SerializeObject(result));
            }
            else
            {
                result = new List<UpdateBPMSTaskRequest>();
            }
            return result;
        }
    }
}
