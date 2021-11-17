using System.Threading.Channels;
using System.Threading.Tasks;

namespace Pipeline
{
    internal class ConsumerRunner<TInput> : IAsyncProcess
    {
        private readonly Channel<TInput> _channel;
        private readonly IConsumer<TInput> _consumer;

        public ConsumerRunner(Channel<TInput> channel, IConsumer<TInput> consumer)
        {
            _channel = channel;
            _consumer = consumer;
        }

        public async Task RunAsync()
        {
            while (await _channel.Reader.WaitToReadAsync())
            {
                if (!_channel.Reader.TryRead(out var input))
                    continue;

                await _consumer.ConsumeAsync(input);
            }
            
            _consumer.Complete();
        }
    }
}