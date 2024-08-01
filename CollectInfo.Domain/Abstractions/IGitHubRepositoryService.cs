using CollectInfo.Domain.Models;

namespace CollectInfo.Domain.Abstractions
{
    public interface IGitHubRepositoryService
    {
        IList<ResultCollectItem> GetResultQuantityLettersInFiles(bool isSeparateTotalByFiles = false);
    }
}
