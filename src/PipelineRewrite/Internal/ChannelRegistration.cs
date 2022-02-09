using PipelineRewrite.Interfaces;

namespace PipelineRewrite.Internal;

internal class ChannelRegistration<T> : IChannelRegistration
{
    public ChannelRegistration(int capacity, int parallelism)
    {
        Capacity = capacity;
        Parallelism = parallelism;
    }

    public int Capacity { get; }
    public int Parallelism { get; }
}