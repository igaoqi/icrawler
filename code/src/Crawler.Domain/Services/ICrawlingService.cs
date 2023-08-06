using System;
using Crawler.Domain.Entities;

namespace Crawler.Domain.Services
{
    public interface ICrawlingService : ITransientDependency
    {
        Task<CrawledData<T>> CrawlDataAsync<T>(string url) where T : class;
    }
}