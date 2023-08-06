using System;

namespace Crawler.Domain.Entities
{
    public class CrawledData<T> where T : class
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public T Data { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}