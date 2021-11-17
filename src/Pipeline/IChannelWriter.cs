using System.Threading.Tasks;

namespace Pipeline
{
    public interface IChannelWriter<TWrite>
    {
        void Complete();
        
        Task WriteAsync(TWrite item);
    }
}