using System;
using System.Net.Http;

namespace githubapi.Controllers
{
    public class GitHubClientProvider
    {
        public HttpClient Client { get; private set; }
        public GitHubClientProvider()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "githubapi");
            Client = client;
        }
    }
}
