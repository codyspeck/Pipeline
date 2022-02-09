namespace PipelineRewrite.Interfaces;

internal interface IChannelRegistration
{
    int Parallelism { get; }
}