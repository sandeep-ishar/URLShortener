using UrlShortener.Domain.Entities;
using UrlShortener.Infrastructure.Repositories;

namespace UrlShortener.Infrastructure.Tests
{
    public class InMemoryUrlRepositoryTests
    {
        [Fact]
        public void Save_StoresUrlMapping()
        {
            // Arrange
            var repo = new InMemoryUrlRepository();
            var mapping = new UrlMapping("test1234", "https://test.com");

            // Act
            repo.Save(mapping);
            var fetched = repo.GetByShortUrlId("test1234");

            // Assert
            Assert.Equal("https://test.com", fetched.OriginalUrl);
        }

        [Fact]
        public void GetByShortId_NotFound_ReturnsNull()
        {
            var repo = new InMemoryUrlRepository();
            var result = repo.GetByShortUrlId("doesnotExist");
            Assert.Null(result);
        }
    }
}