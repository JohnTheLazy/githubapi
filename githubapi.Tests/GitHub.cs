using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace githubapi.Tests
{
    public class GitHub
    {
        [Fact]
        public async Task Users()
        {
            HttpStatusCode actual;

            using (HttpClient client = new Controllers.GitHubClientProvider().Client)
            {
                string userName = "test";
                using HttpResponseMessage response = await client.GetAsync($"/users/{userName}");
                response.EnsureSuccessStatusCode();
                actual = response.StatusCode;
            }

            Assert.Equal(HttpStatusCode.OK, actual);
        }
    }
}
