using System;

namespace SignalRTypingsCreator.Core.Hubs
{
    public class Hub
    {
        public Type HubType {get; internal set; }
        public Type HubClientType { get; internal set; }
    }
}