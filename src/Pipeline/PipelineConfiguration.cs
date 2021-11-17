using System.Collections.Generic;

namespace Pipeline
{
    public class PipelineConfiguration
    {
        internal List<ChannelRegistration> Channels { get; } = new();
        internal List<ConsumerRegistration> Consumers { get; } = new();
        internal List<ProducerRegistration> Producers { get; } = new();

        public PipelineConfiguration WithChannel<TChannel>(int capacity = 0)
        {
            Channels.Add(new ChannelRegistration(typeof(TChannel), capacity));
            return this;
        }

        public PipelineConfiguration WithConsumer<TConsumer, TChannel>(int parallelism = 1)
            where TConsumer : IConsumer<TChannel>
        {
            Consumers.Add(new ConsumerRegistration(typeof(TConsumer), typeof(TChannel), parallelism));
            return this;
        }

        public PipelineConfiguration WithProducer<TProducer, TChannel>()
            where TProducer : IProducer<TChannel>
        {
            Producers.Add(new ProducerRegistration(typeof(TProducer), typeof(TChannel)));
            return this;
        }
    }
}