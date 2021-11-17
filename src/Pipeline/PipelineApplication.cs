using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Pipeline
{
    public class PipelineApplication
    {
        private readonly IServiceCollection _services;

        public PipelineApplication(IServiceCollection services)
        {
            _services = services;
        }

        public async Task RunAsync()
        {
            await using var provider = _services.BuildServiceProvider();
            
            var configuration = provider.GetRequiredService<PipelineConfiguration>();

            var consumerTasks = Task.WhenAll(configuration.Consumers
                .SelectMany(x => provider.GetRequiredServices<IAsyncProcess>(Generics.ConsumerRunner(x.ChannelType), x.Parallelism))
                .Select(x => x.RunAsync())
                .ToList());
            
            var producerTasks = Task.WhenAll(configuration.Producers
                .Select(x => provider.GetRequiredService<IAsyncProcess>(Generics.ProducerRunner(x.ChannelType)))
                .Select(x => x.RunAsync())
                .ToList());
            
            var orchestratorTasks = Task.WhenAll(configuration.Channels
                .Select(x => provider.GetRequiredService<IAsyncProcess>(Generics.ChannelWriterOrchestrator(x.Type)))
                .Select(x => x.RunAsync())
                .ToList());

            await Task.WhenAll(
                consumerTasks,
                producerTasks,
                orchestratorTasks);
        }
    }
}