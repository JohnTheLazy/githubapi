using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace githubapi.Tests
{
    public class ClientProvider
    {
        public HttpClient Client { get; private set; }
        public ClientProvider()
        {
            TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = server.CreateClient();
        }
    }
}
