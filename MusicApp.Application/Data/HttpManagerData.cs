using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace music_app.Data
{
    public class HttpManagerData
    {
        public HttpManagerData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;
        private string URL;

        private HttpClient CreateHttpClient(string token = null)
        {
            URL = _configuration["Api:Url"];

            HttpClient client = null;

            if(!string.IsNullOrEmpty(token)) {
                client = new HttpClient(new HttpClientHandler())
                {
                    DefaultRequestHeaders = { Authorization = new AuthenticationHeaderValue("Bearer", token) }
                };
                return client;
            }

            client = new HttpClient(new HttpClientHandler());
            return client;
        }


        public async Task<T> Post<T, V>(string sequenceUrl, V data, string token = null)
            where T : class
            where V : class
        {
            var client = CreateHttpClient(token);

            var result = await client.PostAsJsonAsync($"{URL}{sequenceUrl}", data);
            var obj = await result.Content.ReadFromJsonAsync<T>();

            return obj;
        }

        public async Task<T> Get<T>(string sequenceUrl, string token = null)
            where T : class
        {
            var client = CreateHttpClient(token);

            var result = await client.GetAsync($"{URL}{sequenceUrl}");
            var obj = await result.Content.ReadFromJsonAsync<T>();

            return obj;
        }

    }
}
