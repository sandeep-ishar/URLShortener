using System.Collections.Concurrent;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.Repositories
{
    public class InMemoryUrlRepository: IUrlRepository
    {
        private static ConcurrentDictionary<string, UrlMapping> _urlDict = new ConcurrentDictionary<string, UrlMapping>();

        public UrlMapping GetByShortUrlId(string shortId)
        {
            _urlDict.TryGetValue(shortId, out var mapping);
            return mapping;
        }

        public void Save(UrlMapping urlMapping)
        {
            _urlDict[urlMapping.ShortId] = urlMapping;
        }
    }
}
