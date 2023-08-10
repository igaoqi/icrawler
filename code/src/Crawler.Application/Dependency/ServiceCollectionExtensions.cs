using System.Reflection;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Yitter.IdGenerator;

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
                services.AddTransient(type);
            }

            return services;
        }

        public static void AddDomainTransientDependency(this ContainerBuilder builder)
        {
            var assemblys = Assembly.Load("Crawler.Infrastructure");
            var baseType = typeof(Domain.Dependency.ITransientDependency);

            builder.RegisterAssemblyTypes(assemblys).Where(p => baseType.IsAssignableFrom(p) && p != baseType).AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        public static IServiceCollection AddIdGenerator(this IServiceCollection services)
        {
            var options = new IdGeneratorOptions(0);
            YitIdHelper.SetIdGenerator(options);

            return services;
        }
    }
}