using System;

namespace Pipeline
{
    internal class ConsumerRegistration
    {
        public Type ConsumerType { get; }
        public Type ChannelType { get; }
        public int Parallelism { get; }

        public ConsumerRegistration(Type consumerType, Type channelType, int parallelism)
        {
            if (parallelism <= 0)
                throw new ArgumentOutOfRangeException(nameof(parallelism), $"{nameof(parallelism)} must be greater than zero");
            
            ConsumerType = consumerType;
            ChannelType = channelType;
            Parallelism = parallelism;
        }
    }
}