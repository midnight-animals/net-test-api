using MongoDB.Driver;
using Moq;
using OnlineDictionary.Services;
using OnlineDictionary.Tests.Fixture;
using Xunit;

namespace OnlineDictionary.Tests.Services
{
    public class TestWordEntryService
    {
        //[Fact]
        //public async Task GetAll_ReturnsExpectedData()
        //{
        //    // Arrange
        //    var expectedData = WordEntryFixture.GetTestWordEntries();
        //    var mockCollection = new Mock<IMongoCollection<WordEntry>>();
        //    var asyncCursor = new Mock<IAsyncCursor<WordEntry>>();

        //    mockCollection
        //        .Setup(col => col.FindAsync(
        //            Builders<WordEntry>.Filter.Empty, 
        //            It.IsAny<FindOptions<WordEntry>>(), 
        //            default))
        //        .Returns(expectedData);

        //    asyncCursor.SetupSequence(_async => _async.MoveNext(default)).Returns(true).Returns(false);
        //    asyncCursor.SetupGet(_async => _async.Current).Returns(expectedData);

        //    var service = new WordEntryService(mockCollection.Object);

        //    // Act
        //    var result = await service.GetAllWordsAsync();

        //    // Assert
        //    Assert.Equal(expectedData, result);
        //}
    }
}