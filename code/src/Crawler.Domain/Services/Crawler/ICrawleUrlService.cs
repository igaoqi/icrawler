﻿using Crawler.Domain.Contracts.CrawleUrl;
using Crawler.Domain.Dependency;
using Crawler.Domain.Entities.CrawleUrl;

namespace Crawler.Domain.Services.Crawler
{
    public interface ICrawleUrlService : ITransientDependency
    {
        Task<bool> AddAsync(CrawleUrl crawleUrl);

        Task<bool> UpdateAsync(CrawleUrl crawleUrl);

        Task<CrawleUrl> GetFirstOrDefaultAsync(CrawleUrlQuery query);

        Task<IEnumerable<CrawleUrl>> GetListAsync(CrawleUrlQuery query);
    }
}