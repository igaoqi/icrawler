using Crawler.Application.Dependency;
using Crawler.Domain.Contracts.CrawleUrl;
using Crawler.Domain.Entities.CrawleUrl;
using Crawler.Domain.Enums.CrawleUrl;
using Crawler.Domain.Services.Crawler;

namespace Crawler.Application.Services
{
    public class CrawleUrlAppService : ITransientSelfDependency
    {
        private readonly ICrawleUrlService _crawleUrlService;

        public CrawleUrlAppService(ICrawleUrlService crawleUrlService)
        {
            _crawleUrlService = crawleUrlService;
        }

        public async Task<bool> AddCrawleUrlAsync(CrawleUrl request)
        {
            var crawleUrl = await _crawleUrlService.GetFirstOrDefaultAsync(new CrawleUrlQuery()
            {
                Url = request.Url
            });

            if (crawleUrl != null)
            {
                return false;
            }

            return await _crawleUrlService.AddAsync(request);
        }

        public async Task<IEnumerable<CrawleUrl>> GetUnCrawledUrlsAsync()
        {
            return await _crawleUrlService.GetListAsync(new CrawleUrlQuery()
            {
                Status = CrawleUrlStatus.UnCrawled
            });
        }

        public async Task MarkAsCrawledAsync(long crawleId)
        {
            var crawleUrl = new CrawleUrl()
            {
                Id = crawleId,
                Status = CrawleUrlStatus.Success
            };

            await _crawleUrlService.UpdateAsync(crawleUrl);
        }
    }
}