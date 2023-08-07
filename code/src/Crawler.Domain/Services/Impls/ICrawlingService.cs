using Crawler.Domain.Http;

namespace Crawler.Domain.Services.Impls
{
    public class CrawlingService : ICrawlingService
    {
        public async Task<CrawleData> CrawlDataAsync(string url)
        {
            Console.WriteLine("CrawlDataAsync");

            return default;
        }
    }
}