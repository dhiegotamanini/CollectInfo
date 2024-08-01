using CollectInfo.Domain.Abstractions;
using CollectInfo.Domain.Models;
using CollectInfo.Domain.Utils;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CollectInfo.Infrastructure
{
    public class GitHubRepository : IGitHubRepository
    {
        private readonly AppSettings _settings;

        public GitHubRepository(AppSettings settings)
        {
            _settings = settings;
        }

        public async Task<string> GetLatestCommitSha()
        {
            var url = $"{_settings.BaseUrl}{_settings.OwnerRepository}/{_settings.Repository}/commits";
            var response = await MakeRequest<List<GitHubCommitDetails>>(url);
            return response.FirstOrDefault().Sha;
        }

        public async Task<GitHubResultTreeRepository> GetRepositoryTree(string sha)
        {
            var url = $"{_settings.BaseUrl}{_settings.OwnerRepository}/{_settings.Repository}/git/trees/{sha}?recursive=1";
            var response = await MakeRequest<GitHubResultTreeRepository>(url);
            return response;
        }

        public async Task<List<GitHubFileContentResult>> GetFilesContent(List<string> filesName)
        {
            var filesContent = new List<GitHubFileContentResult>();
            var tasksToRequest = filesName.Select(async fileName =>
            {
                string url = $"{_settings.BaseUrl}{_settings.OwnerRepository}/{_settings.Repository}/contents/{fileName}";
                var file = await MakeRequest<GitHubFile>(url);
                string base64Content = file.Content.ToString();
                var bytes = Convert.FromBase64String(base64Content);

                return new GitHubFileContentResult
                {
                    Filename = fileName,
                    Content = Encoding.UTF8.GetString(bytes)
                };
            });

            filesContent = (await Task.WhenAll(tasksToRequest)).ToList();
            return filesContent;
        }

        public async Task<List<GitHubFileContentResult>> GetGitWorkingRepositoryTreeFilesContent(string latestCommitSha)
        {
            var gitRepositoryTree = await GetRepositoryTree(latestCommitSha);
            var criteriaExtensionSearchFileType = string.IsNullOrWhiteSpace(_settings.CriteriaExtensionSearchFileType)
                                                     ? new List<string> { ".ts" , ".js" }
                                                     : _settings.CriteriaExtensionSearchFileType?
                                                        .Split(',')
                                                        .Select(ext => ext.Trim().ToLower())
                                                        .ToList();
            var filesNames = gitRepositoryTree.Tree
                                .Where(e => criteriaExtensionSearchFileType.Any(ext => e.Path.ToLower().Trim().EndsWith(ext)))
                                .Select(x => x.Path)
                                .ToList();

            var filesContent = await GetFilesContent(filesNames);
            return filesContent;
        }

        private async Task<T> MakeRequest<T>(string url)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.TokenAccessRepository);
            var response = await client.GetAsync(url);
            
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseBody);
        }
    }

}
