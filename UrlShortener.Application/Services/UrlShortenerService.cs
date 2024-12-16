using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Application.Interfaces;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Services
{
    public class UrlShortenerService :IUrlShortenerService
    {
        private readonly IUrlRepository _repository;
        public UrlShortenerService(IUrlRepository repository) 
        {
            _repository = repository;
        }
        public string GetOriginalUrl(string shortId)
        {
            var result = _repository.GetByShortUrlId(shortId);
            return result?.OriginalUrl;
        }

        public string ShortenUrl(string originalUrl)
        {
            if (!Uri.TryCreate(originalUrl, UriKind.Absolute, out var url))
            {
                throw new ArgumentException("The URL provided is Invalid", nameof(originalUrl));
            }
            string shortId = GenerateShortId();
            var urlMapping = new UrlMapping(shortId, originalUrl);
            _repository.Save(urlMapping);
            return shortId;
        }

        private string GenerateShortId()         
        {
            var guid = Guid.NewGuid();
            var base64 = Convert.ToBase64String(guid.ToByteArray())
                                .Replace("/", "_")
                                .Replace("+", "-")
                                .TrimEnd('=');

            return base64.Substring(0, 8);
        }
    }
}
