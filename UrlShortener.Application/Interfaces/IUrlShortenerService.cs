using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Application.Interfaces
{
    public interface IUrlShortenerService
    {
        string ShortenUrl(string originalUrl);
        string GetOriginalUrl(string shortUrlId);
    }
}
