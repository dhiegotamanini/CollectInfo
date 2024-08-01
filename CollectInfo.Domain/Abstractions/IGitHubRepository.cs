using CollectInfo.Domain.Models;

namespace CollectInfo.Domain.Abstractions
{
    public interface IGitHubRepository
    {
        Task<string> GetLatestCommitSha();
        Task<List<GitHubFileContentResult>> GetGitWorkingRepositoryTreeFilesContent(string latestCommitSha);
    }
}
