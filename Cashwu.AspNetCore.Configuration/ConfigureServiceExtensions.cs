using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cashwu.AspNetCore.Configuration
{
    public static class ConfigureServiceExtensions
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration configuration, string assemblyName)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(IConfiguration));
            }

            if (string.IsNullOrWhiteSpace(assemblyName))
            {
                throw new ArgumentNullException(nameof(assemblyName));
            }

            var types = Reflection.GetAssembliesTypeHasAttributesOf<ConfigurationSectionAttribute>(assemblyName);

            foreach (var type in types)
            {
                var configSection = type.GetCustomAttribute<ConfigurationSectionAttribute>();

                var t = type;

                if (configSection.IsCollections)
                {
                    t = typeof(List<>).MakeGenericType(configSection.CollectionType);
                }

                services.Add(new ServiceDescriptor(t, provider => configuration.GetSection(configSection.Name).Get(t), configSection.Lifetime));
            }

            return services;
        }
    }
}