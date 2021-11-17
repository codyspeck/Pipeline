using System.Threading.Tasks;

namespace Pipeline
{
    internal interface IAsyncProcess
    {
        Task RunAsync();
    }
}