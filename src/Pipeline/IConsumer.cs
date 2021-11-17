using System.Threading.Tasks;

namespace Pipeline
{
    public interface IConsumer<TRead>
    {
        void Complete();
        Task ConsumeAsync(TRead item);
    }
}