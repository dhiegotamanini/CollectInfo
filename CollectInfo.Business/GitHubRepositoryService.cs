using CollectInfo.Domain.Abstractions;
using CollectInfo.Domain.Models;
using System.Net;

namespace CollectInfo.Business
{
    public class GitHubRepositoryService : IGitHubRepositoryService
    {
        private readonly IGitHubRepository _repository;
        public GitHubRepositoryService(IGitHubRepository repository)
        {
            _repository = repository;
        }

        public IList<ResultCollectItem> GetResultQuantityLettersInFiles(bool isSeparateTotalByFiles = false)
        {

            try
            {
                var latestCommitSha = _repository.GetLatestCommitSha().Result;
                if (string.IsNullOrWhiteSpace(latestCommitSha))
                {
                    throw new HttpException((int)HttpStatusCode.BadRequest, "Bad Request: Commit SHA is null");
                }

                var resultColletItems = new List<ResultCollectItem>();
                var filesContent = _repository.GetGitWorkingRepositoryTreeFilesContent(latestCommitSha).Result;
                if (!isSeparateTotalByFiles)
                {
                    string contentAllFiles = string.Join(" ", filesContent.Select(item => item.Content));
                    var resultCollectItem = ReturnCorrectFormat(contentAllFiles);
                    resultColletItems.Add(new ResultCollectItem { Filename = "All JS and TS Files", CollectItems = resultCollectItem });
                    return resultColletItems;
                }

                foreach (var fileContent in filesContent)
                {
                    var itemFormatted = ReturnCorrectFormat(fileContent.Content);
                    resultColletItems.Add(new ResultCollectItem { Filename = fileContent.Filename, CollectItems = itemFormatted });
                }

                return resultColletItems;

            }
            
            catch (Exception ex)
            {
                int statusCode = 0;
                if (ex.InnerException is HttpRequestException httpEx && httpEx.StatusCode.HasValue)
                {
                    statusCode = (int)httpEx.StatusCode.Value;
                }
                
                throw new HttpException(statusCode != 0 ? statusCode : (int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
            }
        }

        private static List<ResultCollect> ReturnCorrectFormat(string content)
        {
            var result = content
                 .Where(char.IsLetter)
                 .GroupBy(c => char.ToLower(c))
                 .Select(group => new ResultCollect
                 {
                     Letter = group.Key.ToString(),
                     CountTotalLetter = group.Count()
                 })
                 .OrderByDescending(result => result.CountTotalLetter)
                 .ToList();

            return result;

        }
    }
}
