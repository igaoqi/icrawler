using Autofac.Extensions.DependencyInjection;
using Crawler.Application.Dependency;
using Crawler.Application.Jobs.Dependency;
using Crawler.Domain.Http;
using Crawler.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
                services.Configure<MysqlConfig>(context.Configuration.GetSection("Database:Mysql"));
                services.Configure<SqlLiteConfig>(context.Configuration.GetSection("Database:Sqlite"));
                services.Configure<List<QuartzConfig>>(context.Configuration.GetSection("Jobs"));

                //注册HttpClient
                services.AddHttpClientAgent();

                //注册Id生成器
                services.AddIdGenerator();

                //注册定时任务
                services.AddJobs(context.Configuration.GetSection("Jobs").Get<List<QuartzConfig>>());
            })
            .ConfigureLogging((context, logging) =>
            {
                logging.AddLog4Net("Configs/log4net.xml");
            })
            .UseServiceProviderFactory(new AutofacServiceProviderFactory(container =>
            {
                container.AddDomainTransientDependency();
                container.AddAppTransientDependency();
            }))
            .Build();

        await builder.RunAsync();
    }
}