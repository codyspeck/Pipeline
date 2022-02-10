using System.Threading.Channels;
using PipelineRewrite.Interfaces;

namespace PipelineRewrite.Runners;

internal class ConsumerRunner<T> : IRunner
{
    private readonly Channel<T> _channel;
    private readonly IConsumer<T> _consumer;

    public ConsumerRunner(Channel<T> channel, IConsumer<T> consumer)
    {
        _channel = channel;
        _consumer = consumer;
    }

    public async Task Run()
    {
        while (await _channel.Reader.WaitToReadAsync())
        {
            if (!_channel.Reader.TryRead(out var item))
                continue;

            await _consumer.Consume(item);
        }
    }
}