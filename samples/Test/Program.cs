using PipelineRewrite;
using PipelineRewrite.Interfaces;

var app = new PipelineApplicationBuilder()
    .AddChannel<MessageA>(1, 1)
    .AddChannel<MessageB>(1, 1)
    .AddChannel<MessageC>(1, 1)
    .AddConsumerAndProducers(typeof(Program).Assembly)
    .Build();
    
await app.Run();

public record MessageA(string message);
public record MessageB(string message);
public record MessageC(string message);

public class Producer : IProducer<MessageA>
{
    public async IAsyncEnumerable<MessageA> Produce()
    {
        Console.WriteLine("Producing A");
        yield return await Task.FromResult(new MessageA("A"));
        Console.WriteLine("Producing B");
        yield return await Task.FromResult(new MessageA("B"));
        Console.WriteLine("Producing C");
        yield return await Task.FromResult(new MessageA("C"));
    }
}

public class Consumer : IConsumer<MessageA>
{
    private readonly IChannelWriter<MessageB> _writerB;
    private readonly IChannelWriter<MessageC> _writerC;

    public Consumer(IChannelWriter<MessageB> writerB, IChannelWriter<MessageC> writerC)
    {
        _writerB = writerB;
        _writerC = writerC;
    }

    public async Task Consume(MessageA item)
    {
        Console.WriteLine($"Consuming {item}");
        await _writerB.Write(new MessageB(new Random().NextInt64(60).ToString()));
        await _writerC.Write(new MessageC(new Random().NextInt64(60).ToString()));
    }
}

public class SecondConsumer : IConsumer<MessageB>
{
    public Task Consume(MessageB item)
    {
        Console.WriteLine($"Consuming {item}");
        return Task.CompletedTask;
    }
}

public class ThirdConsumer : IConsumer<MessageC>
{
    public Task Consume(MessageC item)
    {
        Console.WriteLine($"Consuming {item}");
        return Task.CompletedTask;
    }
}