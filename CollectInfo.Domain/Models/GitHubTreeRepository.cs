namespace CollectInfo.Domain.Models
{
    public class GitHubTreeRepository
    {
        public string Path { get; set; }
        public string Mode { get; set; }
        public string Type { get; set; }
        public string Sha { get; set; }
        public string Size { get; set; }
        public string Url { get; set; }
    }
}
