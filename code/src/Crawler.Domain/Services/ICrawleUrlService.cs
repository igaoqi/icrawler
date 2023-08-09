using Crawler.Domain.Dependency;
using Crawler.Domain.Entities;

namespace Crawler.Domain.Services
{
    public interface ICrawleUrlService : ITransientDependency
    {
        Task<IEnumerable<CrawleUrl>> GetUnCrawledUrlsAsync();
    }
}