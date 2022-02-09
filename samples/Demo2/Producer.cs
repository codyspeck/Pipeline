using PipelineRewrite.Interfaces;

class Producer : IProducer<string>
{
    private readonly string _message;

    public Producer(string message)
    {
        _message = message;
    }

    public async IAsyncEnumerable<string> Produce()
    {
        yield return await Task.FromResult($"{_message} 1");
        yield return await Task.FromResult($"{_message} 2");
        yield return await Task.FromResult($"{_message} 3");
    }
}