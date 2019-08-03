using System;
using System.Collections.Generic;
using System.Text;

namespace BPMSApi.Model.BPMSTask
{
    public class BPMSTaskData
    {
        public String Assignee { get; set; }
        public DateTime CreateTime { get; set; }
        public String Description { get; set; }
        public DateTime DueDate { get; set; }
        public Int32 Id { get; set; }
        public String Name { get; set; }
    }
}
