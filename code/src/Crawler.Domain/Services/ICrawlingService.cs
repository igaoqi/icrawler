using Crawler.Domain.Http;

namespace Crawler.Domain.Services
{
    public interface ICrawlingService : ITransientDependency
    {
        Task<CrawleData> CrawlDataAsync(string url);
    }
}