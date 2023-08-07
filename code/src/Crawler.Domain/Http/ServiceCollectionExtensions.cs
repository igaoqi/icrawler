using Microsoft.Extensions.DependencyInjection;

namespace Crawler.Domain.Http
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHttpClientAgent(this IServiceCollection services)
        {
            services.AddHttpClient();
        }
    }
}