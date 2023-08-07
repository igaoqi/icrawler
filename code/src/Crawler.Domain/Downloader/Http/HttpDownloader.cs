using Crawler.Domain.Downloader.Http;
using Crawler.Domain.Http;

namespace Crawler.Domain.Downloader.HttpDownloader
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

            if (request.HttpMethod == HttpMethod.Get)
            {
                return await GetCrawleDataAsync(client, request);
            }

            throw new Exception();
        }

        private async Task<CrawleData> GetCrawleDataAsync(HttpClient client, CrawleRequest request)
        {
            var response = await client.GetAsync(request.Url);
            var data = await response.Content.ReadAsStringAsync();

            return new CrawleData
            {
                Url = request.Url,
                HttpStatusCode = response.StatusCode,
                Data = data,
                CreatedAt = DateTime.UtcNow
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

            return client;
        }
    }
}