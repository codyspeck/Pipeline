using PipelineRewrite.Interfaces;

namespace PipelineRewrite.Runners;

internal class ProducerRunner<T> : IRunner
{
    private readonly IChannelWriter<T> _channel;
    private readonly IProducer<T> _producer;

    public ProducerRunner(IChannelWriter<T> channel, IProducer<T> producer)
    {
        _channel = channel;
        _producer = producer;
    }

    public async Task Run()
    {
        await foreach (var item in _producer.Produce())
        {
            await _channel.Write(item);
        }
    }
}