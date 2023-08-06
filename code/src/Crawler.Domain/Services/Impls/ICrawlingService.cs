using System;
using Crawler.Domain.Entities;

namespace Crawler.Domain.Services.Impls
{
    public class CrawlingService : ICrawlingService
    {
        public Task<CrawledData<T>> CrawlDataAsync<T>(string url) where T : class
        {
            throw new NotImplementedException();
        }
    }
}