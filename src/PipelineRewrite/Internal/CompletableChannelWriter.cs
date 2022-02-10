using System.Threading.Channels;
using PipelineRewrite.Interfaces;

namespace PipelineRewrite.Internal;

internal class CompletableChannelWriter<T> : IChannelWriter<T>, IDisposable
{
    private readonly Channel<T> _channel;
    private readonly TaskCompletionSource _completion;

    public CompletableChannelWriter(Channel<T> channel, ChannelOrchestrator<T> orchestrator)
    {
        _channel = channel;
        _completion = new();
        Console.WriteLine($"Subscribing {typeof(T)}");
        orchestrator.Subscribe(_completion.Task);
    }

    public async Task Write(T item)
    {
        await _channel.Writer.WriteAsync(item);
    }

    public void Dispose()
    {
        Console.WriteLine($"Disposing {typeof(T)}");
        _completion.SetResult();
    }
}