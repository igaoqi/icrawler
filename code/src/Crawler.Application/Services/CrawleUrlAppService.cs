using Crawler.Application.Dependency;
using Crawler.Domain.Entities;
using Crawler.Domain.Services;

namespace Crawler.Application.Services
{
    public class CrawleUrlAppService : ITransientDependency
    {
        private readonly ICrawleUrlService _crawleUrlService;

        public CrawleUrlAppService(ICrawleUrlService crawleUrlService)
        {
            _crawleUrlService = crawleUrlService;
        }

        public async Task<IEnumerable<CrawleUrl>> GetUnCrawledUrlsAsync()
        {
            return await _crawleUrlService.GetUnCrawledUrlsAsync();
        }
    }
}