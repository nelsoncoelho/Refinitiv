using BPMSApi.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace BPMSApi
{
    public class BPMSConfig
    {
        // If the API uses authentication by an API key.
        public String ApiKey { get; set; }

        // If the API uses authentication by username and password.
        public BPMSCredentials Credentials { get; set; }

        // If the API uses a bearer token.
        public BPMSGetTokenResponse Token { get; set; }
    }
}
