using Crawler.Domain.Downloader;
using Crawler.Domain.Enums.CrawleUrl;
using Crawler.Domain.Http;

namespace Crawler.Infrastructure.Downloader
{
    public class HttpDownloader : IHttpDownloader
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpDownloader(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public string Name => throw new NotImplementedException();

        public async Task<CrawleData> DownloadAsync(CrawleRequest request)
        {
            var client = CreateHttpClient();

            if (request.HttpMethod == CrawleMethod.Get)
            {
                return await GetCrawleDataAsync(client, request);
            }

            throw new Exception();
        }

        private static async Task<CrawleData> GetCrawleDataAsync(HttpClient client, CrawleRequest request)
        {
            var response = await client.GetAsync(request.Url);
            var content = await response.Content.ReadAsStringAsync();

            return new CrawleData
            {
                Url = request.Url,
                HttpStatusCode = response.StatusCode,
                Data = content,
                CrawledAt = DateTime.UtcNow
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="timeout">request times out (senconds)</param>
        /// <returns></returns>
        private HttpClient CreateHttpClient(int timeout = 30)
        {
            var client = _httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(timeout);
            client.DefaultRequestHeaders.Add("User-Agent", UserAgentHelper.GetUserAgent());
            return client;
        }
    }
}