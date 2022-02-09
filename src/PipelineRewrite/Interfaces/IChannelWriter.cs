namespace PipelineRewrite.Interfaces;

public interface IChannelWriter<in T>
{
    Task Write(T item);
}