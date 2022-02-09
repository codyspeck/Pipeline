using System.Threading.Channels;
using PipelineRewrite.Interfaces;

namespace PipelineRewrite.Runners;

public class ProducerRunner<T> : IRunner
{
    private readonly Channel<T> _channel;
    private readonly IProducer<T> _producer;

    public ProducerRunner(Channel<T> channel, IProducer<T> producer)
    {
        _channel = channel;
        _producer = producer;
    }

    public async Task Run()
    {
        await foreach (var item in _producer.Produce())
        {
            await _channel.Writer.WriteAsync(item);
        }

        _channel.Writer.Complete();
    }
}