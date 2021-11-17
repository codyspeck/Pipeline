using System.Threading.Tasks;

namespace Pipeline
{
    internal class ProducerRunner<TWrite> : IAsyncProcess
    {
        private readonly IChannelWriter<TWrite> _writer;
        private readonly IProducer<TWrite> _producer;

        public ProducerRunner(IChannelWriter<TWrite> writer, IProducer<TWrite> producer)
        {
            _writer = writer;
            _producer = producer;
        }

        public async Task RunAsync()
        {
            await foreach (var item in _producer.ProduceAsync())
            {
                await _writer.WriteAsync(item);
            }

            _writer.Complete();
        }
    }
}