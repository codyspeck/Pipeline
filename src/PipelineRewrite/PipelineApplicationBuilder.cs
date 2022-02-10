using System.Reflection;
using System.Threading.Channels;
using Microsoft.Extensions.DependencyInjection;
using PipelineRewrite.Interfaces;
using PipelineRewrite.Internal;
using PipelineRewrite.Runners;

namespace PipelineRewrite;

public class PipelineApplicationBuilder
{
    private readonly IServiceCollection _services = new ServiceCollection()
        .AddScoped(typeof(ChannelOrchestrator<>))
        .AddTransient(typeof(ConsumerRunner<>))
        .AddTransient(typeof(ProducerRunner<>))
        .AddTransient(typeof(IChannelWriter<>), typeof(CompletableChannelWriter<>));

    public PipelineApplicationBuilder Configure(Action<IServiceCollection> act)
    {
        act(_services);
        return this;
    }

    public PipelineApplicationBuilder AddChannel<T>(int capacity, int parallelism)
    {
        _services
            .AddSingleton(_ => new ChannelRegistration(capacity, parallelism, typeof(T)))
            .AddSingleton(_ => Channel.CreateBounded<T>(capacity));
            
        return this;
    }
    
    public PipelineApplicationBuilder AddConsumerAndProducers(Assembly assembly)
    {
        _services
            .Scan(scan => scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IProducer<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IConsumer<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

        return this;
    }

    public PipelineApplication Build()
    {
        return new PipelineApplication(_services);
    }
}