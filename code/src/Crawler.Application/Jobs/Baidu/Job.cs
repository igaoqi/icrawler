using Crawler.Domain.Downloader.Http;
using Crawler.Domain.Http;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Crawler.Application.Jobs.Baidu
{
    [DisallowConcurrentExecution]
    public class Job : IDependencyJob
    {
        private readonly ILogger<Job> _logger;
        private readonly IHttpDownloader _httpDownloader;

        public Job(ILogger<Job> logger, IHttpDownloader httpDownloader)
        {
            _logger = logger;
            _httpDownloader = httpDownloader;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var data = await _httpDownloader.DownloadAsync(new CrawleRequest
            {
                Url = "https://www.baidu.com",
                HttpMethod = HttpMethod.Get
            });

            _logger.LogInformation($"{data.Data}");
        }
    }
}