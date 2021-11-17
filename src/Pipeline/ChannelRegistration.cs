using System;

namespace Pipeline
{
    internal class ChannelRegistration
    {
        public Type Type { get; }
        public int Capacity { get; }

        public ChannelRegistration(Type type, int capacity)
        {
            Type = type;
            Capacity = capacity;
        }
    }
}