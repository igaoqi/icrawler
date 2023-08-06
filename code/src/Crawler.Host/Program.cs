using Crawler.Domain.Dependency;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);

        builder.ConfigureServices((context, services) =>
        {
            services.AddTransientDependency();
        }).Build().RunAsync();
    }
}