namespace PipelineRewrite.Internal;

internal class ChannelRegistration
{
    public ChannelRegistration(int capacity, int parallelism, Type type)
    {
        Capacity = capacity;
        Parallelism = parallelism;
        Type = type;
    }

    public int Capacity { get; }
    public int Parallelism { get; }
    public Type Type { get; }
}