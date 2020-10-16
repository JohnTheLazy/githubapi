namespace githubapi.Models
{
    public class GitHubUser
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Company { get; set; }
        public int Followers { get; set; }
        public int Public_Repos { get; set; }
        public int Average_Followers { get => Public_Repos > 0 ? Followers / Public_Repos : 0; }
    }
}
