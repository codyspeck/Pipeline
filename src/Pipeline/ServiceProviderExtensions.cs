using System;
using System.Collections.Generic;

namespace Pipeline
{
    internal static class ServiceProviderExtensions
    {
        internal static T GetRequiredService<T>(this IServiceProvider sp)
        {
            return (T) sp.GetRequiredService(typeof(T));
        }
        
        internal static T GetRequiredService<T>(this IServiceProvider sp, Type type)
        {
            return (T) sp.GetRequiredService(type);
        }
        
        internal static IEnumerable<T> GetRequiredServices<T>(this IServiceProvider sp, Type type, int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return (T) sp.GetRequiredService(type);
            }
        }
        
        private static object GetRequiredService(this IServiceProvider sp, Type type)
        {
            var service = sp.GetService(type);

            if (service == null)
                throw new InvalidOperationException($"Unable to resolve service: {type}");

            return service;
        }
    }
}