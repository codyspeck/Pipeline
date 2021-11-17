using System;

namespace Pipeline
{
    internal static class Generics
    {
        public static Type ConsumerRunner(Type type)
        {
            return typeof(ConsumerRunner<>).MakeGenericType(type);
        }

        public static Type ProducerRunner(Type type)
        {
            return typeof(ProducerRunner<>).MakeGenericType(type);
        }

        public static Type ChannelWriterOrchestrator(Type type)
        {
            return typeof(ChannelWriterOrchestrator<>).MakeGenericType(type);
        }
    }
}