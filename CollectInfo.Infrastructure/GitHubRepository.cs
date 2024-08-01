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
        private readonly string _token = "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";

        public GitHubRepository(AppSettings settings)
        {
            _settings = settings;
            //_token = DecodeBase64(_settings.TokenAccessRepository);
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
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");
            var response = await client.GetAsync(url);
            
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseBody);
        }

        public static string EncodeBase64(string token)
        {
            var bytes = Encoding.UTF8.GetBytes(token);
            return Convert.ToBase64String(bytes);
        }

        public static string DecodeBase64(string base64Token)
        {
            var bytes = Convert.FromBase64String(base64Token);
            return Encoding.UTF8.GetString(bytes);
        }

    }

}
