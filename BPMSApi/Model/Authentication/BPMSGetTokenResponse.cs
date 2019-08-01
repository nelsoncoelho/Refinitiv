using System;
using System.Collections.Generic;
using System.Text;

namespace BPMSApi.Model.Authentication
{
    public class BPMSGetTokenResponse
    {
        public String AccessToken { get; set; }
        public String TokenType { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
