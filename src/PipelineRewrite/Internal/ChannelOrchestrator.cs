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

    public void Subscribe(Task task)
    {
        _tasks.Add(task);
    }
    
    public async Task Run()
    {
        while (_tasks.Any(_ => !_.IsCompleted))
            await Task.WhenAll(_tasks);

        Console.WriteLine($"Closing {typeof(T)}");
        _channel.Writer.Complete();
    }
}