using Crawler.Domain.Contracts.CrawleUrl;
using Crawler.Domain.Entities.CrawleUrl;
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

        public async Task<bool> AddAsync(CrawleUrl crawleUrl)
        {
            return await _crawleUrlRepository.InsertAsync(crawleUrl) > 0;
        }

        public async Task<CrawleUrl> GetFirstOrDefaultAsync(CrawleUrlQuery query)
        {
            var sql = $"SELECT * FROM CrawleUrl {query}";

            return await _crawleUrlRepository.GetFirstOrDefaultAsync(sql, query);
        }

        public async Task<IEnumerable<CrawleUrl>> GetListAsync(CrawleUrlQuery query)
        {
            var sql = $"SELECT * FROM CrawleUrl {query}";

            return await _crawleUrlRepository.GetListAsync(sql, query);
        }
    }
}