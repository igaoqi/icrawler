using Crawler.Domain.Options;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Crawler.Application.Jobs.Dependency
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJobs(this IServiceCollection services, List<QuartzConfig>? Jobs)
        {
            services.AddQuartz(options =>
            {
                if (Jobs?.Any() != true)
                {
                    return;
                }

                options.UseDefaultThreadPool(options =>
                {
                    options.MaxConcurrency = 10;
                });

                foreach (var job in Jobs)
                {
                    Type type = Type.GetType(job.Type);
                    string name = type?.FullName ?? throw new ArgumentNullException(nameof(type.FullName));
                    options.AddJob(type, new JobKey(name), x =>
                    {
                        x.DisallowConcurrentExecution(true);
                        x.WithIdentity(name);
                        x.WithDescription(name);
                    });

                    options.AddTrigger(options =>
                    {
                        options.ForJob(name);
                        options.WithIdentity($"{name}_trigger");
                        options.WithCronSchedule(job.Cron);
                        options.StartNow();
                    });
                }
            });

            services.AddQuartzHostedService(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.AwaitApplicationStarted = true;
                options.WaitForJobsToComplete = true;
                options.StartDelay = TimeSpan.FromSeconds(3);
            });

            return services;
        }
    }
}