using System;
using System.Threading.Channels;

namespace Pipeline
{
    internal static class ChannelFactory
    {
        public static object CreateChannel(ChannelRegistration registration)
        {
            return registration.Capacity > 0
                ? CreateBoundedChannel(registration.Type, registration.Capacity)
                : CreateUnboundedChannel(registration.Type);
        }
        
        private static object CreateBoundedChannel(Type type, int capacity)
        {
            // ReSharper disable once PossibleNullReferenceException
            return typeof(Channel)
                .GetMethod("CreateBounded", new [] { typeof(int) })
                .MakeGenericMethod(type)
                .Invoke(null, new object[] { capacity });
        }
        
        private static object CreateUnboundedChannel(Type type)
        {
            // ReSharper disable once PossibleNullReferenceException
            return typeof(Channel)
                .GetMethod("CreateUnbounded", Array.Empty<Type>())
                .MakeGenericMethod(type)
                .Invoke(null, Array.Empty<object>());
        }
    }
}