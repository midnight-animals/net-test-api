using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using net_test_api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Moq;
using System.Net.Security;
using net_test_api.Tests.Fixture;

namespace net_test_api.Tests
{
    public class WordEntryControllerTests
    {
        [Fact]
        public async Task GetAllWords_OnSuccess_ReturnsListOfWordEntry()
        {
            // Arrange
            var wordEntries = WordEntryFixture.GetTestWordEntries();
            var mockWordEntryService = new Mock<IWordEntryService>();
            mockWordEntryService
                .Setup(service => service.GetAllWordsAsync())
                .ReturnsAsync(wordEntries);

            var controller = new WordEntryController(mockWordEntryService.Object);

            // Act
            var result = await controller.GetAllWords();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedWordEntries = Assert.IsAssignableFrom<List<WordEntry>>(okResult.Value);
            Assert.Equal(wordEntries.Count, returnedWordEntries.Count);
            Assert.Equal(wordEntries[0], returnedWordEntries[0]);
        }
        [Fact]
        public async Task GetAllWords_OnSuccess_InvokesWordEntryServiceExactlyOnce()
        {
            // Arrange
            var mockWordEntryService = new Mock<IWordEntryService>();
            mockWordEntryService
                .Setup(service => service.GetAllWordsAsync())
                .ReturnsAsync(WordEntryFixture.GetTestWordEntries);

            var controller = new WordEntryController(mockWordEntryService.Object);

            // Act
            var result = await controller.GetAllWords();

            // Assert
            mockWordEntryService.Verify(
                service => service.GetAllWordsAsync(),
                Times.Once()
            );
        }
        [Fact]
        public async Task GetAllWords_OnNoWords_Returns404()
        {
            // Arrange
            var mockWordEntryService = new Mock<IWordEntryService>();
            mockWordEntryService
                .Setup(service => service.GetAllWordsAsync())
                .ReturnsAsync(new List<WordEntry>());

            var controller = new WordEntryController(mockWordEntryService.Object);

            // Act
            var result = await controller.GetAllWords();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}
