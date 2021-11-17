using System;

namespace Pipeline
{
    internal class ProducerRegistration
    {
        public Type ProducerType { get; }
        public Type ChannelType { get; }

        public ProducerRegistration(Type producerType, Type channelType)
        {
            ProducerType = producerType;
            ChannelType = channelType;
        }
    }
}