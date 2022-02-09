namespace PipelineRewrite.Interfaces;

public interface IConsumer<in T>
{
    Task Consume(T item);
}