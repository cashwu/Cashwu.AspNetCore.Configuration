using System;
using Microsoft.Extensions.DependencyInjection;

namespace Cashwu.AspNetCore.Configuration
{
    public class ConfigurationSectionAttribute : Attribute
    {
        public ConfigurationSectionAttribute(string name, ServiceLifetime lifetime = ServiceLifetime.Scoped, bool isCollections = false, Type collectionType = null)
        {
            Lifetime = lifetime;
            Name = name;
            IsCollections = isCollections;
            CollectionType = collectionType;
        }

        public string Name { get; }

        public ServiceLifetime Lifetime { get; }

        public bool IsCollections { get; }

        public Type CollectionType { get; }
    }
}