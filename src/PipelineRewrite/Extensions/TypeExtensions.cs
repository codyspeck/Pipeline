using PipelineRewrite.Interfaces;
using PipelineRewrite.Internal;
using PipelineRewrite.Runners;

namespace PipelineRewrite.Extensions;

internal static class TypeExtensions
{
    public static bool IsGenericConsumer(this Type type)
    {
        if (!type.IsGenericType)
            return false;

        return type.GetGenericTypeDefinition() == typeof(IConsumer<>);
    }
    
    public static bool IsGenericProducer(this Type type)
    {
        if (!type.IsGenericType)
            return false;

        return type.GetGenericTypeDefinition() == typeof(IProducer<>);
    }

    public static Type GetFirstGenericArgument(this Type type)
    {
        return type.GetGenericArguments().First();
    }
    
    public static Type MakeGenericChannelOrchestratorType(this Type type)
    {
        return typeof(ChannelOrchestrator<>).MakeGenericType(type.GetFirstGenericArgument());
    }
    
    public static Type MakeGenericConsumerRunnerType(this Type type)
    {
        return typeof(ConsumerRunner<>).MakeGenericType(type.GetFirstGenericArgument());
    }
    
    public static Type MakeGenericProducerRunnerType(this Type type)
    {
        return typeof(ProducerRunner<>).MakeGenericType(type.GetFirstGenericArgument());
    }
}