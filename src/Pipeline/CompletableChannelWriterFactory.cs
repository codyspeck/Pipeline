using System;

namespace Pipeline
{
    internal class CompletableChannelWriterFactory<TWrite>
    {
        private readonly IServiceProvider _sp;

        public CompletableChannelWriterFactory(IServiceProvider sp)
        {
            _sp = sp;
        }

        public CompletableChannelWriter<TWrite> CreateCompletableChannelWriter()
        {
            return _sp.GetRequiredService<CompletableChannelWriter<TWrite>>(typeof(CompletableChannelWriter<>).MakeGenericType(typeof(TWrite)));
        }
    }
}