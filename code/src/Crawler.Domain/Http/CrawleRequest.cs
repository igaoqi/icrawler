namespace Crawler.Domain.Http
{
    public class CrawleRequest
    {
        public string Url { get; set; }

        public HttpMethod HttpMethod { get; set; } = HttpMethod.Get;
    }
}