using PipelineRewrite.Interfaces;

class Consumer : IConsumer<string>
{
    public Task Consume(string item)
    {
        Console.WriteLine($"Received {item}");
        return Task.CompletedTask;
    }
}