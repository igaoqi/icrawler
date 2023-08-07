using System.Net;

namespace Crawler.Domain.Http
{
    public class CrawleData
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public string Data { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}