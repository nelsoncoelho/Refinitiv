using BPMSApi.Model.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BPMSApi.Repositories
{
    public abstract class BaseRepository
    {
        public BPMSApiInstance Instance { get; set; }
        public String BaseUrl { get; } = "“http://bpms.com";
        public HttpClient Client { get; set; }

        protected BaseRepository(BPMSApiInstance instance)
        {
            Instance = instance;
            Client = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => true
            });
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // To be used if a custom API Key is used.
            // Client.DefaultRequestHeaders.Add("Custom-Api-Key", instance.Config.ApiKey);

            // To be used
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Instance.Config.Token.AccessToken}");
        }

        // Gets the token.
        private async Task GetToken()
        {
            // Transformation of a set of credentials in order to be 
            var credentials = JsonConvert.SerializeObject(Instance.Config.Credentials);
            var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(credentials));

            // Allows the ContentType of application/json.
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // Call to the token function along with the credentials content.
            var response = await Client.PostAsync($"{BaseUrl}/getTokenFunction", byteContent).Result.Content.ReadAsStringAsync();

            // Deserialization of the token response back to our config to be used later.
            Instance.Config.Token = JsonConvert.DeserializeObject<BPMSGetTokenResponse>(response);
        }

        // Verifies that the token is still valid, and if not, renews it.
        private async Task ValidateToken()
        {
            if (Instance.Config.Token?.AccessToken != null)
            {
                if (Instance.Config.Token.ExpirationDate >= DateTime.Now)
                {
                    await GetToken();
                }
            }
            else
            {
                await GetToken();
            }

            Client.DefaultRequestHeaders.Remove("Authorization");
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Instance.Config.Token.AccessToken}");
        }

        // Gets the token for the calls.
        protected async Task<T> Get<T>(String url)
        {
            var response = await Client.GetAsync($"{BaseUrl}/{url}").Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(response);
        }

        protected async Task<T> Post<T>(String url, Object content)
        {
            var myContent = JsonConvert.SerializeObject(content);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.PostAsync($"{BaseUrl}/{url}", byteContent).Result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}
