using System.Collections.ObjectModel;
using System.Threading.Channels;
using PipelineRewrite.Interfaces;

namespace PipelineRewrite.Internal;

internal class ChannelOrchestrator<T> : IRunner
{
    private readonly Channel<T> _channel;
    private readonly Collection<Task> _tasks;

    public ChannelOrchestrator(Channel<T> channel)
    {
        _channel = channel;
        _tasks = new();
    }

    public IChannelWriter<T> CreateChannelWriter()
    {
        var writer = new CompletableChannelWriter<T>(_channel);
        
        _tasks.Add(writer.Completion);
        
        return writer;
    }
    
    public async Task Run()
    {
        while (_tasks.Any(_ => !_.IsCompleted))
            await Task.WhenAll(_tasks);

        _channel.Writer.Complete();
    }
}