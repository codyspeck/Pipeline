using System;
using System.Threading.Tasks;
using Demo.Messages;
using Pipeline;

namespace Demo.Consumers
{
    public class MemoConsumer : IConsumer<Memo>
    {
        public void Complete()
        {
        }

        public Task ConsumeAsync(Memo item)
        {
            Console.WriteLine(item.Content);
            
            return Task.CompletedTask;
        }
    }
}