using System.Threading.Tasks;
using Demo.Messages;
using Pipeline;

namespace Demo.Consumers
{
    public class ProductConsumer : IConsumer<Product>
    {
        private readonly IChannelWriter<Memo> _memoWriter;

        public ProductConsumer(IChannelWriter<Memo> memoWriter)
        {
            _memoWriter = memoWriter;
        }

        public void Complete()
        {
            _memoWriter.Complete();
        }

        public async Task ConsumeAsync(Product item)
        {
            await _memoWriter.WriteAsync(new Memo
            {
                Content = $"Consuming product {item.Id} - {item.Sku}"
            });
        }
    }
}