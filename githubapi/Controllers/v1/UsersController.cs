using githubapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace githubapi.Controllers.v1
{
    [Route("v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IMemoryCache _cache;
        private MemoryCacheEntryOptions _cacheOptions;

        public UsersController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
            _cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(2));
        }

        private string GetKey(string method, string param)
        {
            return $"{method}-{param}";
        }

        [HttpPost]
        [Route("retrieveUsers")]
        public async Task<ActionResult<List<GitHubUser>>> RetrieveUsers(List<string> userNames)
        {
            List<GitHubUser> results = new List<GitHubUser>();

            foreach (string userName in userNames)
            {
                string key = GetKey("RetrieveUsers", userName);
                GitHubUser result = _cache.Get<GitHubUser>(key);

                if (result != null)
                {
                    results.Add(result);
                }
                else
                {
                    using HttpClient client = new GitHubClientProvider().Client;
                    using HttpResponseMessage response = await client.GetAsync($"/users/{userName}");
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                        byte[] byteArray = buffer.ToArray();
                        string content = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
                        result = JsonConvert.DeserializeObject<GitHubUser>(content);
                        results.Add(result);
                        _cache.Set(key, result, _cacheOptions);
                    }
                }
            }

            if (results.Any())
            {
                results = results.OrderBy(x => x.Name).ToList();
            }

            return results;
        }
    }
}
