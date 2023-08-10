using Crawler.Application.Services;
using Crawler.Domain.Entities.CrawleUrl;
using Microsoft.Extensions.Logging;
using Quartz;
using Yitter.IdGenerator;

namespace Crawler.Application.Jobs.Crawler
{
    [DisallowConcurrentExecution]
    public class CreateTaskJob : IDependencyJob
    {
        private readonly ILogger<CreateTaskJob> _logger;
        private readonly CrawleUrlAppService _crawleUrlAppService;

        public CreateTaskJob(ILogger<CreateTaskJob> logger, CrawleUrlAppService crawleUrlAppService)
        {
            _logger = logger;
            _crawleUrlAppService = crawleUrlAppService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var urls = GetUrls();

            if (!urls.Any())
            {
                return;
            }

            foreach (var item in urls)
            {
                await _crawleUrlAppService.AddCrawleUrlAsync(new CrawleUrl
                {
                    Url = item,
                    CreatedTime = DateTime.Now,
                    Id = YitIdHelper.NextId(),
                    Status = 0,
                    CrawledAt = DateTime.Now,
                    Remark = "test",
                    Retry = 0
                });
            }
        }

        private List<string> GetUrls()
        {
            List<string> Urls = new List<string>();

            Urls.Add("https://www.163.com/v/video/VPAMUFTH8.html");
            Urls.Add("https://www.163.com/dy/article/IBKVDERQ05129QAF.html");
            Urls.Add("https://www.163.com/news/article/IBL6Q09P0001899O.html");

            return Urls;
        }
    }
}