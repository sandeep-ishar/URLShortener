
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Interfaces
{
    public interface IUrlRepository
    {
        UrlMapping GetByShortUrlId(string shortUrlId);
        void Save(UrlMapping urlMapping);
    }
}
