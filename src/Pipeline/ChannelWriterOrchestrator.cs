using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Pipeline
{
    internal class ChannelWriterOrchestrator<TWrite> : IAsyncProcess
    {
        private readonly Channel<TWrite> _channel;
        private readonly CompletableChannelWriterFactory<TWrite> _factory;
        private readonly List<Task> _tasks;

        public ChannelWriterOrchestrator(Channel<TWrite> channel, CompletableChannelWriterFactory<TWrite> factory)
        {
            _channel = channel;
            _factory = factory;
            _tasks = new List<Task>();
        }

        public IChannelWriter<TWrite> CreateChannelWriter()
        {
            var writer = _factory.CreateCompletableChannelWriter();
            
            _tasks.Add(writer.CompletionStatus);

            return writer;
        }

        public async Task RunAsync()
        {
            while (_tasks.Any(x => !x.IsCompleted))
            {
                await Task.WhenAll(_tasks);
            }

            _channel.Writer.Complete();
        }
    }
}