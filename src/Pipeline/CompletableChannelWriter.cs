using System.Threading.Channels;
using System.Threading.Tasks;

namespace Pipeline
{
    internal class CompletableChannelWriter<TWrite> : IChannelWriter<TWrite>
    {
        private readonly Channel<TWrite> _channel;
        private readonly TaskCompletionSource _completion;

        public CompletableChannelWriter(Channel<TWrite> channel)
        {
            _channel = channel;
            _completion = new TaskCompletionSource();
        }

        public Task CompletionStatus => _completion.Task;

        public void Complete()
        {
            _completion.SetResult();
        }
        
        public async Task WriteAsync(TWrite item)
        {
            await _channel.Writer.WriteAsync(item);
        }
    }
}