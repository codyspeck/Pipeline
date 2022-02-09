using System.Threading.Channels;
using PipelineRewrite.Interfaces;

namespace PipelineRewrite.Internal;

internal class CompletableChannelWriter<T> : IChannelWriter<T>, IDisposable
{
    private readonly Channel<T> _channel;
    private readonly TaskCompletionSource _completion;

    public CompletableChannelWriter(Channel<T> channel)
    {
        _channel = channel;
        _completion = new();
    }

    public Task Completion => _completion.Task;

    public async Task Write(T item)
    {
        await _channel.Writer.WriteAsync(item);
    }

    public void Dispose()
    {
        _completion.SetResult();
    }
}