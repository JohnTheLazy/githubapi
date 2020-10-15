using githubapi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace githubapi.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            if (Request.Query.ContainsKey("q"))
            {
                string q = Request.Query["q"].ToString().ToLower();
                List<string> userNames = q.Split(',').Select(x => x.Trim()).ToList();

                DateTime startDate = DateTime.Now;

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
                    List<GitHubUser> users = new List<GitHubUser>();
                    try
                    {
                        users = JsonConvert.DeserializeObject<List<GitHubUser>>(content);
                    }
                    catch
                    {
                    }
                    ViewBag.Result = users;
                }

                TimeSpan speed = DateTime.Now - startDate;
                ViewBag.Speed = speed;
            }

            return View();
        }
    }
}
