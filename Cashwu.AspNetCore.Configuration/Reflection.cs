using System;
using System.Collections.Generic;
using System.Linq;

namespace Cashwu.AspNetCore.Configuration
{
    internal class Reflection
    {
        public static IEnumerable<Type> GetAssembliesTypeHasAttributesOf<T>(string prefixAssemblyName)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic);
            return assembly.Where(a => a.FullName.StartsWith(prefixAssemblyName))
                           .SelectMany(a => a.DefinedTypes.Where(t => t.GetCustomAttributes(typeof(T), true).Length > 0));
        }
    }
}