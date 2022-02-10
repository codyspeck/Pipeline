using System.Formats.Asn1;
using Microsoft.Extensions.DependencyInjection;
using PipelineRewrite.Extensions;
using PipelineRewrite.Interfaces;
using PipelineRewrite.Internal;

namespace PipelineRewrite;

public class PipelineApplication
{
    private readonly IServiceCollection _services;

    internal PipelineApplication(IServiceCollection services)
    {
        _services = services;
    }

    public async Task Run()
    {
        await using var provider = _services.BuildServiceProvider();

        var consumerTasks = Task.WhenAll(_services
            .Where(_ => _.ServiceType.IsGenericConsumer())
            .Select(_ =>
            {
                using var scope = provider.CreateScope();
                // todo: Account for configured parallelism
                var runner = (IRunner) scope.ServiceProvider.GetRequiredService(_.ServiceType.MakeGenericConsumerRunnerType());
                return runner.Run();
            })
            .ToList());
        
        var producerTasks = Task.WhenAll(_services
            .Where(_ => _.ServiceType.IsGenericProducer())
            .Select(_ =>
            {
                using var scope = provider.CreateScope();
                var runner = (IRunner) scope.ServiceProvider.GetRequiredService(_.ServiceType.MakeGenericProducerRunnerType());
                return runner.Run();
            })
            .ToList());
        
        var orchestratorTasks = Task.WhenAll(_services
            .Where(_ => _.ServiceType.IsGenericConsumer())
            .Select(_ => (IRunner) provider.GetRequiredService(_.ServiceType.MakeGenericChannelOrchestratorType()))
            .Select(_ => _.Run())
            .ToList());

        await Task.WhenAll(
            producerTasks,
            consumerTasks,
            orchestratorTasks);
    }
}