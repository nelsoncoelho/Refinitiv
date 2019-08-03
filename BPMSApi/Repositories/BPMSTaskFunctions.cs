using BPMSApi.Model.BPMSTask;
using System;
using System.Collections.Generic;
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
            var result = await Get<GetBPMSTaskResponse>("tasks");
            return result;
        }

        public async Task<GetBPMSTaskResponse> UpdateBPMSTask(UpdateBPMSTaskRequest request)
        {
            var result = await Put<GetBPMSTaskResponse>($"tasks/{request.Id.ToString()}", request);
            return result;
        }
    }
}
