using System.Threading.Tasks;

namespace Pipeline
{
    internal class ChannelWriterProxy<TWrite> : IChannelWriter<TWrite>
    {
        private readonly IChannelWriter<TWrite> _writer;

        public ChannelWriterProxy(ChannelWriterOrchestrator<TWrite> orchestrator)
        {
            _writer = orchestrator.CreateChannelWriter();
        }

        public void Complete()
        {
            _writer.Complete();
        }

        public Task WriteAsync(TWrite item)
        {
            return _writer.WriteAsync(item);
        }
    }
}