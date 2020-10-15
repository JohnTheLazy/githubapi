using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace githubapi.Models
{
    public class GitHubUser
    {
        public string login { get; set; }
        public string name { get; set; }
        public string company { get; set; }
        public string public_repos { get; set; }
        public string followers { get; set; }
    }
}
