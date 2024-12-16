namespace UrlShortener.Domain.Entities
{
    public class UrlMapping
    {
        public UrlMapping(string shortId, string originalUrl)
        {
            ShortId = shortId;
            OriginalUrl = originalUrl;
        }

        public string ShortId { get; set; }
        public string OriginalUrl { get; set; }

    }
}
