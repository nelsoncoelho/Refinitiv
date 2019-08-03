using BPMSApi.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPMSWebApplication.Configs
{
    public interface IBPMSSettings
    {
        String ApiKey { get; }
        BPMSCredentials Credentials { get; }
    }
}
