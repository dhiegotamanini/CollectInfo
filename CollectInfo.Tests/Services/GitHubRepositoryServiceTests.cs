using CollectInfo.Business;
using CollectInfo.Domain.Abstractions;
using CollectInfo.Domain.Models;
using Moq;
using System.Net;

namespace CollectInfo.Tests.Services
{
    public class GitHubRepositoryServiceTests
    {
        private Mock<IGitHubRepository> _repository;
        private GitHubRepositoryService _service;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IGitHubRepository>();
            _service = new GitHubRepositoryService(_repository.Object);
        }

        [Test]
        public void ShouldReturnTotalQuantityLetterWhenSepareteTotalByFilesIsFalse()
        {
            // Arrange
            var commitSha = "commit-sha";
            var filesContent = new List<GitHubFileContentResult>
            {
                new GitHubFileContentResult { Filename = "file1.js", Content = "content1" },
                new GitHubFileContentResult { Filename = "file2.js", Content = "content2" }
            };

            _repository.Setup(repo => repo.GetLatestCommitSha().Result).Returns(commitSha);
            _repository.Setup(repo => repo.GetGitWorkingRepositoryTreeFilesContent(commitSha).Result).Returns(filesContent);

            var result = _service.GetResultQuantityLettersInFiles(isSeparateTotalByFiles: false);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Filename, Is.EqualTo("All JS and TS Files"));
        }

        [Test]
        public void GetResultQuantityLettersInFiles_ShouldThrowHttpException_WhenGetGitWorkingRepositoryTreeFilesContentFails()
        {
            
            var commitSha = "commit-sha";
            _repository.Setup(repo => repo.GetLatestCommitSha()).ReturnsAsync(commitSha);
            _repository.Setup(repo => repo.GetGitWorkingRepositoryTreeFilesContent(commitSha))
                .Throws(new HttpRequestException("Failed to get files content"));

            var exception = Assert.ThrowsAsync<HttpException>(async () =>
            {
                _service.GetResultQuantityLettersInFiles();
            });

            Assert.That(exception.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
            Assert.That(exception.Message, Is.EqualTo("Error: Failed to get files content"));
        }

        [Test]
        public void ShouldReturnSucessWhenCallMethodOnce()
        {
            var listExpected = new List<GitHubFileContentResult>()
            {
                new GitHubFileContentResult(){ Content = "AAbl" , Filename = "file1.js"  },
                new GitHubFileContentResult(){ Content = "xhsotl" , Filename = "file2.js"  }
            };

            var commitSha = "commit-sha";
            _repository.Setup(repo => repo.GetLatestCommitSha()).ReturnsAsync(commitSha);
            _repository.Setup(repo => repo.GetGitWorkingRepositoryTreeFilesContent(commitSha).Result).Returns(listExpected);
            _service.GetResultQuantityLettersInFiles();
            _repository.Verify(x => x.GetLatestCommitSha(), Times.Once);
        }


        [Test]
        public void ShouldReturnExceptionWhenShaIsNullOrEmpty()
        {
            var commitSha = "";
            var ex = Assert.Throws<HttpException>(() => _service.GetResultQuantityLettersInFiles());
            Assert.That(ex.Message, Is.EqualTo("Error: Bad Request: Commit SHA is null"));
        }
    }
}
