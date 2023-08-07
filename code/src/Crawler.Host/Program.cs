using Crawler.Domain.DependencyExtensions;
using Crawler.Domain.Http;
using Crawler.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory()).AddCommandLine(args);
                config.AddJsonFile("Configs/Database.json", optional: true);
            })
            .ConfigureServices((context, services) =>
            {
                //注册HttpClient
                services.AddHttpClientAgent();
                //注册依赖
                services.AddTransientDependency();
                //绑定配置项
                services.Configure<MysqlOptions>(context.Configuration.GetSection("MysqlConfig"));
            })
            .Build();

        await builder.RunAsync();
    }
}