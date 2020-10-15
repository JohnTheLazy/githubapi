namespace githubapi.Models
{
    public class GitHubUser
    {
        public string name { get; set; }
        public string login { get; set; }
        public string company { get; set; }
        public int followers { get; set; }
        public int public_repos { get; set; }
        public int average_followers { get => followers / public_repos; }
    }
}
