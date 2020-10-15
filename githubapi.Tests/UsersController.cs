using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace githubapi.Tests
{
    public class UsersController
    {
        [Fact]
        public async Task RetrieveUsers()
        {
            HttpStatusCode actual;

            using (HttpClient client = new ClientProvider().Client)
            {
                List<string> userNames = new List<string>();
                string content = JsonConvert.SerializeObject(userNames);
                using (StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
                {
                    using (HttpResponseMessage response = await client.PostAsync("/v1/users/retrieveUsers", stringContent))
                    {
                        response.EnsureSuccessStatusCode();
                        actual = response.StatusCode;
                    }
                }
            }

            Assert.Equal(HttpStatusCode.OK, actual);
        }
    }
}
