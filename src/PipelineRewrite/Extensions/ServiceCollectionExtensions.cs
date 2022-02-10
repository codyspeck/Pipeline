using Microsoft.Extensions.DependencyInjection;

namespace PipelineRewrite.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IEnumerable<Type> GetProducerRunnerTypes(this ServiceCollection services)
    {
        throw new NotImplementedException();
        
        foreach (var service in services)
        {
        }
    }
}