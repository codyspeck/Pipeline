using Microsoft.Extensions.DependencyInjection;
using PipelineRewrite.Interfaces;

namespace PipelineRewrite;

public class PipelineApplication
{
    private readonly IServiceProvider _serviceProvider;
    
    internal PipelineApplication(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Run()
    {
        var producerTasks = Task.WhenAll(
            _serviceProvider
                .GetServices(typeof(IProducer<>))
                .Select(_ => (IRunner) _serviceProvider.GetRequiredService(typeof(IProducer<>)))
                .Select(_ => _.Run()));

        var consumerTasks = Task.WhenAll(
            _serviceProvider
                .GetServices(typeof(IConsumer<>))
                .Select(_ => (IRunner) _serviceProvider.GetRequiredService(typeof(IConsumer<>)))
                .Select(_ => _.Run()));

        await Task.WhenAll(producerTasks, consumerTasks);
    }
}