using System;
using System.Collections.Generic;
using System.Text;

namespace BPMSApi.Model.BPMSTask
{
    public class UpdateBPMSTaskRequest
    {
        public String Approver { get; set; }
        public DateTime ApprovalTime { get; set; } = DateTime.Now;
        // Only used if approved is false.
        public String RejectionReason { get; set; }
        public Boolean Approved { get; set; }
        public Int32 Id { get; set; }
    }
}
