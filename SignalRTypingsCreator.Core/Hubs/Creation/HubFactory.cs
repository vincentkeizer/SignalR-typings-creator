using System;

namespace SignalRTypingsCreator.Core.Hubs.Creation
{
    public class HubFactory
    {
        public Hub Create(Type type)
        {
            return new Hub
            {
                HubType = type
            };
        }
    }
}