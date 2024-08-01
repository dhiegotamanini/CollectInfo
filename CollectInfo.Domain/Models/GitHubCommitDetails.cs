namespace CollectInfo.Domain.Models
{
    public class GitHubCommitDetails
    {
        public string Sha { get; set; }
        public string Node_Id { get; set; }
        public dynamic Commit { get; set; }
    }
}
