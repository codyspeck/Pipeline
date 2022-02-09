using Microsoft.Extensions.DependencyInjection;
using PipelineRewrite;

await new PipelineApplicationBuilder()
    .Configure(_ => _
        .AddTransient(_ => "Message"))
    .AddChannel<string>(1, 10)
    .AddConsumerAndProducers(typeof(Program).Assembly)
    .Build()
    .Run();