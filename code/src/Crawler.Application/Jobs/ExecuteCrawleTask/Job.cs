using Crawler.Application.Contracts;
using Crawler.Application.Services;
using Crawler.Domain.Downloader;
using Crawler.Domain.Enums.CrawleUrl;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Crawler.Application.Jobs.ExecuteCrawleTask
{
    internal class Job : IDependencyJob
    {
        private readonly ILogger<Job> _logger;
        private readonly IHttpDownloader _httpDownloader;
        private readonly CrawleUrlAppService _crawleUrlAppService;
        private readonly HtmlDefaultParseAppService<NetEaseNewsParseData> _htmlDefaultParseAppService;

        public Job(ILogger<Job> logger,
            IHttpDownloader httpDownloader,
            CrawleUrlAppService crawleUrlAppService,
            HtmlDefaultParseAppService<NetEaseNewsParseData> htmlDefaultParseAppService)
        {
            _logger = logger;
            _httpDownloader = httpDownloader;
            _crawleUrlAppService = crawleUrlAppService;
            _htmlDefaultParseAppService = htmlDefaultParseAppService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var crawleUrls = await _crawleUrlAppService.GetUnCrawledUrlsAsync();
            if (crawleUrls?.Any() != true)
            {
                return;
            }

            foreach (var item in crawleUrls)
            {
                var crawleData = await _httpDownloader.DownloadAsync(new Domain.Http.CrawleRequest()
                {
                    Url = item.Url,
                    HttpMethod = CrawleMethod.Get
                });

                var parseData = await ParseNetEaseNewsData(crawleData.Data);

                if (parseData.Success)
                {
                    await _crawleUrlAppService.MarkAsCrawledAsync(item.Id);
                }
            }
        }

        private async Task<NetEaseNewsParseData> ParseNetEaseNewsData(string content)
        {
            return await _htmlDefaultParseAppService.ParseAsync(content);
        }
    }
}