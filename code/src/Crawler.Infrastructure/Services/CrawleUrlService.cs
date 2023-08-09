using Crawler.Domain.Entities;
using Crawler.Domain.Repository;
using Crawler.Domain.Services;

namespace Crawler.Infrastructure.Services
{
    public class CrawleUrlService : ICrawleUrlService
    {
        private readonly ICrawleUrlRepository _crawleUrlRepository;

        public CrawleUrlService(ICrawleUrlRepository crawleUrlRepository)
        {
            _crawleUrlRepository = crawleUrlRepository;
        }

        public Task<IEnumerable<CrawleUrl>> GetUnCrawledUrlsAsync()
        {
            return _crawleUrlRepository.GetListAsync();
        }
    }
}