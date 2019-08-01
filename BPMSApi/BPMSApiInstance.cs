using System;

namespace BPMSApi
{
    public class BPMSApiInstance
    {
        public BPMSConfig Config { get; set; }

        public BPMSApiInstance(BPMSConfig config)
        {
            config = Config;
        }
    }
}
