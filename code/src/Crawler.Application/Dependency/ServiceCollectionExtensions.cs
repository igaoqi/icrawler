using System.Reflection;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Yitter.IdGenerator;

namespace Crawler.Application.Dependency
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppTransientDependency(this ContainerBuilder builder)
        {
            // Get all types in the assembly
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            var selfDependencyTypes = types.Where(t => typeof(ITransientSelfDependency).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            foreach (var type in selfDependencyTypes)
            {
                if (!type.IsGenericType)
                {
                    builder.RegisterType(type).AsSelf().InstancePerDependency();
                }
                else
                {
                    builder.RegisterGeneric(type).AsSelf().InstancePerDependency();
                }
            }
        }

        public static void AddDomainTransientDependency(this ContainerBuilder builder)
        {
            var assembly = Assembly.Load("Crawler.Infrastructure");
            var types = assembly.GetTypes();

            var selfDependencyTypes = types.Where(t => typeof(Domain.Dependency.ITransientDependency).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            foreach (var type in selfDependencyTypes)
            {
                if (!type.IsGenericType)
                {
                    builder.RegisterType(type).AsImplementedInterfaces().InstancePerDependency();
                }
                else
                {
                    var typeInterface = type.GetInterfaces().FirstOrDefault();
                    if (typeInterface == null)
                    {
                        continue;
                    }

                    builder.RegisterGeneric(type).As(typeInterface).InstancePerDependency();

                    //builder.RegisterGeneric(type).AsImplementedInterfaces().InstancePerDependency();
                }
            }
        }

        public static IServiceCollection AddIdGenerator(this IServiceCollection services)
        {
            var options = new IdGeneratorOptions(0);
            YitIdHelper.SetIdGenerator(options);

            return services;
        }

        public static IServiceCollection AddLog4net(this IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddLog4Net("Configs/log4net.xml");
            });

            return services;
        }
    }
}