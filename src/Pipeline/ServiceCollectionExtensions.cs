using System;
using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;

namespace Pipeline
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPipeline(this IServiceCollection services, Func<PipelineConfiguration, PipelineConfiguration> fn)
        {
            var configuration = fn(new PipelineConfiguration());

            // Add Channels
            foreach (var cfg in configuration.Channels)
                services.AddSingleton(MakeGenericChannel(cfg.Type), ChannelFactory.CreateChannel(cfg));

            // Add Consumers
            foreach (var cfg in configuration.Consumers)
                services.AddTransient( MakeGenericConsumer(cfg.ChannelType), cfg.ConsumerType);
            
            // Add Producers
            foreach (var cfg in configuration.Producers)
                services.AddTransient(MakeGenericProducer(cfg.ChannelType), cfg.ProducerType);

            return services
                .AddSingleton(configuration)
                .AddSingleton(typeof(ChannelWriterOrchestrator<>))
                .AddSingleton(typeof(CompletableChannelWriterFactory<>))
                .AddTransient(typeof(CompletableChannelWriter<>))
                .AddTransient(typeof(ConsumerRunner<>))
                .AddTransient(typeof(ProducerRunner<>))
                .AddTransient(typeof(IChannelWriter<>), typeof(ChannelWriterProxy<>));
        }

        public static PipelineApplication BuildPipelineApplication(this IServiceCollection services)
        {
            return new PipelineApplication(services);
        }
        
        private static Type MakeGenericChannel(Type type)
        {
            return typeof(Channel<>).MakeGenericType(type);
        }
        
        private static Type MakeGenericConsumer(Type type)
        {
            return typeof(IConsumer<>).MakeGenericType(type);
        }
        
        private static Type MakeGenericProducer(Type type)
        {
            return typeof(IProducer<>).MakeGenericType(type);
        }
    }
}