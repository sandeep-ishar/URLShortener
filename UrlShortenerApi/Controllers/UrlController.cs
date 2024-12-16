using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Interfaces;
using URLShortenerApi.Models;

namespace UrlShortnerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlShortenerService _shortenerService;
        private readonly IConfiguration _config;
        public UrlController(IUrlShortenerService shortenerService, IConfiguration config)
        {
            _shortenerService = shortenerService;
            _config = config;
        }

        [HttpPost("shorten")]
        public IActionResult ShortenUrl([FromBody] ShortenRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.OriginalUrl))
            {
                return BadRequest("The request must contain a 'url in the originalUrl' field.");
            }

            try
            {
                var shortId = _shortenerService.ShortenUrl(request.OriginalUrl);
                var domain = _config["ShortDomain"] ?? "http://testdomain.com";
                var response = new ShortenResponse
                {
                    ShortId = shortId,
                    ShortUrl = $"{domain}{shortId}"
                };
                return Ok(response);
            }
            catch (ArgumentException ex)
            {                
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {                
                return BadRequest("Invalid URL.");
            }
        }

        [HttpGet("{shortId}")]
        public IActionResult ResolveUrl(string shortId)
        {
            if (string.IsNullOrWhiteSpace(shortId))
            {
                return BadRequest("short Url is required.");
            }

            var originalUrl = _shortenerService.GetOriginalUrl(shortId);
            if (originalUrl == null)
            {
                return NotFound("No URL found for the given shortId.");
            }

            var response = new ResolveResponse { OriginalUrl = originalUrl };
            return Ok(response);
        }
    }
}
