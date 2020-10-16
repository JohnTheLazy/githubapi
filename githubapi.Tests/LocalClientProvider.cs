using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;

namespace githubapi.Tests
{
    public class LocalClientProvider
    {
        public HttpClient Client { get; private set; }
        public LocalClientProvider()
        {
            TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = server.CreateClient();
        }
    }
}