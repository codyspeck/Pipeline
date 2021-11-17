using System.Collections.Generic;

namespace Pipeline
{
    public interface IProducer<TWrite>
    {
        IAsyncEnumerable<TWrite> ProduceAsync();
    }
}