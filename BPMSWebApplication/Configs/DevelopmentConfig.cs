using BPMSApi.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPMSWebApplication.Configs
{
    public class DevelopmentConfig : IBPMSSettings
    {
        // If the API uses authentication by an API key.
        public String ApiKey { get; set; } = "YourAPIKey";

        // If the API uses authentication by username and password.
        public BPMSCredentials Credentials { get; set; } = new BPMSCredentials
        {
            Username = "YourUsername",
            Password = "YourPassword"
        };
    }
}
