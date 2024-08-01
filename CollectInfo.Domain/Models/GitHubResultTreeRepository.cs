namespace CollectInfo.Domain.Models
{
    public class GitHubResultTreeRepository
    {
        public string Sha { get; set; }
        public string Url { get; set; }
        public List<GitHubTreeRepository> Tree { get; set; }
    }
}
