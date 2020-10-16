using System;
using System.Net.Http;

namespace githubapi.Controllers
{
    public class ClientProvider
    {
        private readonly string APPLICATION_URL = "http://localhost:2685";
        public HttpClient Client { get; private set; }

        public ClientProvider()
        {
            HttpClient client = new HttpClient();          
            client.BaseAddress = new Uri(APPLICATION_URL);
            Client = client;
        }
    }
}
