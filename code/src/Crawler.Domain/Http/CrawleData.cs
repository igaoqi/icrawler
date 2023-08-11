using System.Net;

namespace Crawler.Domain.Http
{
    public class CrawleData
    {
        public long Id { get; set; } = 0;

        public string Url { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public string Data { get; set; }

        public DateTime CrawledAt { get; set; }
    }
}