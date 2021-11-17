using System.Threading.Tasks;
using Demo.Consumers;
using Demo.Messages;
using Demo.Producers;
using Microsoft.Extensions.DependencyInjection;
using Pipeline;

namespace Demo
{
    public static class Program
    {
        public static async Task Main()
        {
            await new ServiceCollection()
                .AddPipeline(p => p
                    .WithChannel<Memo>()
                    .WithChannel<Product>()
                    .WithConsumer<MemoConsumer, Memo>()
                    .WithConsumer<ProductConsumer, Product>()
                    .WithProducer<ProductProducer, Product>())
                .BuildPipelineApplication()
                .RunAsync();
        }
    }
}