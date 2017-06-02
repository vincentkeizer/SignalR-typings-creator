using System;

namespace SignalRTypingsCreator.Core.Hubs.Creation
{
    public class HubFactory
    {
        public Hub Create(Type hubType, Type hubClientType)
        {
            return new Hub
            {
                HubType = hubType,
                HubClientType = hubClientType,
            };
        }
    }
}