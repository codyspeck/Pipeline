using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Messages;
using Pipeline;

namespace Demo.Producers
{
    public class ProductProducer : IProducer<Product>
    {
        public async IAsyncEnumerable<Product> ProduceAsync()
        {
            yield return await Task.FromResult(new Product {Id = 1, Sku = "W100"});
            yield return await Task.FromResult(new Product {Id = 2, Sku = "W200"});
            yield return await Task.FromResult(new Product {Id = 3, Sku = "W300"});
        }
    }
}