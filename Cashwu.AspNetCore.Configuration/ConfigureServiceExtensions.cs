using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cashwu.AspNetCore.Configuration
{
    public static class ConfigureServiceExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration, string prefixAssemblyName)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(IConfiguration));
            }

            var types = Reflection.GetAssembliesTypeOf<IConfig>(prefixAssemblyName);

            foreach (var type in types)
            {
                var configSection = type.GetCustomAttribute<ConfigurationSectionAttribute>();

                var t = type;

                if (configSection.IsCollections)
                {
                    t = typeof(List<>).MakeGenericType(configSection.CollectionType ?? type);
                }

                services.Add(new ServiceDescriptor(t, provider => configuration.GetSection(configSection.Name).Get(t), configSection.Lifetime));
            }

            return services;
        }
    }
}