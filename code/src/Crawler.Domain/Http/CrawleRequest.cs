using Crawler.Domain.Enums.CrawleUrl;

namespace Crawler.Domain.Http
{
    public class CrawleRequest
    {
        public string Url { get; set; }

        public CrawleMethod HttpMethod { get; set; } = CrawleMethod.Get;
    }
}