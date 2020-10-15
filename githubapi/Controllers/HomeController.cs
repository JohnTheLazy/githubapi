using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using githubapi.Models; 
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace githubapi.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [HttpGet, HttpPost]
        public async Task<IActionResult> Index()
        {
            if (Request.Query.ContainsKey("q"))
            {
                string q = Request.Query["q"].ToString().ToLower();
                List<string> userNames = q.Split(',').Select(x => x.Trim()).ToList();

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response =
                        await client.PostAsync(
                            "http://localhost:2685/v1/users/retrieveUsers",
                            new StringContent(JsonConvert.SerializeObject(userNames),
                            Encoding.UTF8,
                            "application/json"));

                    byte[] buffer = await response.Content.ReadAsByteArrayAsync();
                    byte[] byteArray = buffer.ToArray();
                    string content = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
                    List<GitHubUser> users = JsonConvert.DeserializeObject<List<GitHubUser>>(content);
                    ViewBag.Result = users;
                }
            }

            return View();
        }
    }
}
