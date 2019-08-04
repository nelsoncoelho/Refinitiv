using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using BPMSApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BPMSApiUnitTests
{
    public abstract class TestBase
    {
        public BPMSApiInstance Instance { get; set; }


        // Ideally these would have they're own project to escalate the number of secrets per API.
        #region Secrets definition
        private static AmazonSecretsManagerClient Client = new AmazonSecretsManagerClient(RegionEndpoint.EUWest2);
        private static async Task<T> GetSecrets<T>(String secretName)
        {
            var secret = await Client.GetSecretValueAsync(new GetSecretValueRequest
            {
                SecretId = secretName
            });
            return JsonConvert.DeserializeObject<T>(secret.SecretString);
        }

        public static async Task<BPMSConfig> GetBPMSSecrets() => await GetSecrets<BPMSConfig>("BPMSCredentials");

        #endregion

        
        protected TestBase()
        {
            // Credentials would be gathered using this function.
            // var credentials = GetBPMSSecrets().Result;

            Instance = new BPMSApiInstance(new BPMSConfig
            {
                // Here you can pass the configurations for authentication of the API call.
                // ApiKey = credentials.ApiKey,
                // Credentials = credentials.Credentials
            });
        }

    }
}
