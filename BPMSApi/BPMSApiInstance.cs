using BPMSApi.Repositories;
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

        public BPMSTaskFunctions BPMSTaskFunctions
        {
            get
            {
                return new BPMSTaskFunctions(this);
            }
        }

        public AuthenticationFunctions AuthenticationFunctions
        {
            get
            {
                return new AuthenticationFunctions(this);
            }
        }
    }
}
