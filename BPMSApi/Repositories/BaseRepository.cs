using BPMSApi.Model.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BPMSApi.Repositories
{
    public abstract class BaseRepository
    {
        public BPMSApiInstance Instance { get; set; }
        public String BaseUrl { get; } = "“http://bpms.com";
        public HttpClient Client { get; set; }
        public List<String> FilesToBeCopied { get; set; } = new List<String>
        {
            "Tasks",
            "Users",
            "Approvals"
        };

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

            // To be used if a bearer token is required for the call.
            // Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Instance.Config.Token.AccessToken}");

            // Function that generates the file for the tests.
            // WriteBaseJson();
        }

        // In order to avoid invalid paths for the file, this function should generate one.
        public void WriteBaseJson()
        {
            FilesToBeCopied.ForEach(f =>
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = $"BPMSApi.Model.ApiData.{f}.json";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                using (StreamReader reader = new StreamReader(stream))
                {
                    var result = reader.ReadToEnd();
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + $"\\{f}.json", result);
                }
            });
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
            // The below would be used to access the API and Get the data.
            // var response = await Client.GetAsync($"{BaseUrl}/{url}").Result.Content.ReadAsStringAsync();

            var response = GetJson(url);
            return JsonConvert.DeserializeObject<T>(response);
        }

        protected async Task<T> Post<T>(String url, Object content)
        {
            // The below would be used to access the API and make POST requests to it.
            // var response = await Client.PostAsync($"{BaseUrl}/{url}", GetByteArrayContent(content)).Result.Content.ReadAsStringAsync();
            var response = JsonConvert.SerializeObject(content);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + $"\\{url}.json", response);
            return JsonConvert.DeserializeObject<T>(response);
        }

        protected async Task<T> Put<T>(String url, Object content)
        {
            // The below would be used to access the API and make PUT requests to it.
            // var response = await Client.PutAsync($"{BaseUrl}/{url}", GetByteArrayContent(content)).Result.Content.ReadAsStringAsync();
            var response = GetJson(url);
            return JsonConvert.DeserializeObject<T>(response);
        }

        private String GetJson(String file)
        {
            using (StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + $"\\{file}.json"))
            {
                return reader.ReadToEnd();
            }
        }

        private ByteArrayContent GetByteArrayContent(Object content)
        {
            var myContent = JsonConvert.SerializeObject(content);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
        }
    }
}
