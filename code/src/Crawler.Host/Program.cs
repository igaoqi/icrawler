using Crawler.Application.Jobs.Dependency;
using Crawler.Domain.Dependency;
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
                config.AddJsonFile("Configs/QuartzConfig.json", optional: true);
            })
            .ConfigureServices(async (context, services) =>
            {
                //绑定配置项
                services.Configure<MysqlConfig>(context.Configuration.GetSection("MysqlConfig"));
                services.Configure<List<QuartzConfig>>(context.Configuration.GetSection("Jobs"));

                //注册HttpClient
                services.AddHttpClientAgent();

                //注册依赖
                services.AddTransientDependency();

                //注册定时任务
                await services.AddJobs(context.Configuration.GetSection("Jobs").Get<List<QuartzConfig>>());
            })
            .Build();

        await builder.RunAsync();
    }
}