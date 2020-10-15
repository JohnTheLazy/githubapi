using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using githubapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace githubapi.Controllers.v1
{
    [Route("v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet, HttpPost]
        [Route("retrieveUsers")]
        public async Task<ActionResult<List<GitHubUser>>> RetrieveUsers(List<string> userNames)
        {
            List<GitHubUser> users = new List<GitHubUser>();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("User-Agent", "GitHubUsers");

                HttpResponseMessage response = await client.GetAsync($"https://api.github.com/users/{userNames.FirstOrDefault()}");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                    byte[] byteArray = buffer.ToArray();
                    string content = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
                    GitHubUser user = JsonConvert.DeserializeObject<GitHubUser>(content);
                    users.Add(user);
                }
            }

            return users;
        }
    }
}
