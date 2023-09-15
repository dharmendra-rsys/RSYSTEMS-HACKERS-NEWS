using HackerNewsApp.API.Controllers;
using HackersNewApp.DataAccess.Models;
using HackersNewApp.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Any;
using Moq;

namespace HackerNewsApp.Test.Controllers
{
    public class HackerControllerTests
    {
        private HackersController hackersController;
        private Mock<IHackersRepository> hackerRepositoryMock;
        private IMemoryCache memoryCache;

        public List<int> storyIds = new List<int>
        {
            90355, 88365, 85881, 88742, 88723, 87955, 10403
        };

        public HackersStory story = new HackersStory()
        {
            Title = "test_title",
            Url = "test_url"
        };

        [SetUp]
        public void Setup()
        {
            // arrange
            hackerRepositoryMock = new Mock<IHackersRepository>();
            memoryCache = new MemoryCache(new MemoryCacheOptions());
            hackersController = new HackersController(hackerRepositoryMock.Object, memoryCache);
        }

        [Test]
        public void GetNewestStoriesAsync_VerifyAll_Test()
        {
            // Arrange
            hackerRepositoryMock.Setup(p => p.NewestStoryIdsAsync("")).ReturnsAsync(storyIds);
            hackerRepositoryMock.Setup(p => p.GetStoryByIdAsyc(0000)).ReturnsAsync(story);

            // Act
            var response = hackersController.GetNewestStoriesAsync("").Result;
            var result = response as OkObjectResult;
            var resultData = new List<HackersStory>();
            if (result != null && result.Value != null)
            {
                resultData = (List<HackersStory>)result.Value;
            }

            // Assert
            Assert.NotNull(response);
            Assert.That(200, Is.EqualTo(result?.StatusCode));
            Assert.That(7, Is.EqualTo(resultData?.Count));
        }

        [Test]
        public void GetStoryByIdAsync_VerifyAll_Test()
        {
            // Arrange
            hackerRepositoryMock.Setup(p => p.GetStoryByIdAsyc(00001)).ReturnsAsync(story);

            // Act
            var response = hackersController.GetStoryByIdAsync(00001).Result;

            // Assert
            Assert.NotNull(response);
            Assert.That(response, Is.EqualTo(story));
        }
    }
}