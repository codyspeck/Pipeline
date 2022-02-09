namespace PipelineRewrite.Interfaces;

public interface IProducer<out T>
{
    IAsyncEnumerable<T> Produce();
}