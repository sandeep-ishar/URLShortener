using Moq;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Services;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Tests
{
    public class UrlShortenerServiceTests
    {
        private readonly Mock<IUrlRepository> _repoMock;
        private readonly UrlShortenerService _service;

        public UrlShortenerServiceTests()
        {
            _repoMock = new Mock<IUrlRepository>();
            _service = new UrlShortenerService(_repoMock.Object);
        }

        [Fact]
        public void ShortenUrl_ValidUrl_SavesAndReturnsShortUrl()
        {
            // Arrange
            var validUrl = "https://www.wordandbrown.com/";

            // Act
            var shortId = _service.ShortenUrl(validUrl);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(shortId));
            _repoMock.Verify(repo => repo.Save(It.Is<UrlMapping>(m => m.OriginalUrl == validUrl)), Times.Once);
        }

        [Fact]
        public void ShortenUrl_InvalidUrl_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _service.ShortenUrl("not_a_url"));
        }

        [Fact]
        public void GetOriginalUrl_ExistingShortUrl_ReturnsOriginalUrl()
        {
            var shortId = "test123";
            var originalUrl = "https://wordandbrown.com";

            _repoMock.Setup(repo => repo.GetByShortUrlId(shortId))
                     .Returns(new UrlMapping(shortId, originalUrl));

            var result = _service.GetOriginalUrl(shortId);

            Assert.Equal(originalUrl, result);
        }

        [Fact]
        public void GetOriginalUrl_NonExistingShortUrl_ReturnsNull()
        {
            var result = _service.GetOriginalUrl("random");
            Assert.Null(result);
        }
    }
}