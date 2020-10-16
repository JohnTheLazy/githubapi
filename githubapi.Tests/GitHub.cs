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

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("User-Agent", "githubapi");

                string userName = "test";
                using (HttpResponseMessage response = await client.GetAsync($"https://api.github.com/users/{userName}"))
                {
                    response.EnsureSuccessStatusCode();
                    actual = response.StatusCode;
                }
            }

            Assert.Equal(HttpStatusCode.OK, actual);
        }
    }
}
