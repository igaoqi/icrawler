﻿using Crawler.Domain.Dependency;
using Crawler.Domain.Http;

namespace Crawler.Domain.Downloader
{
    public interface IHttpDownloader : ITransientDependency
    {
        string Name { get; }

        Task<CrawleData> DownloadAsync(CrawleRequest request);
    }
}