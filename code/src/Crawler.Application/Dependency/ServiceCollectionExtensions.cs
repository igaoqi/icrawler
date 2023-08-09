using Microsoft.Extensions.DependencyInjection;

namespace Crawler.Application.Dependency
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppTransientDependency(this IServiceCollection services)
        {
            var assembly = typeof(ITransientDependency).Assembly;
            var transientTypes = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && typeof(ITransientDependency).IsAssignableFrom(t));

            foreach (var type in transientTypes)
            {
                //var serviceType = type.GetInterfaces().FirstOrDefault(i => i != typeof(ITransientDependency));
                //if (serviceType == null)
                //{
                //    continue;
                //}

                services.AddTransient(type);
            }

            return services;
        }
    }
}