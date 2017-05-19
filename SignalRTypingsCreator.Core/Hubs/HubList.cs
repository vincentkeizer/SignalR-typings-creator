using System.Collections.Generic;

namespace SignalRTypingsCreator.Core.Hubs
{
    public class HubList
    {
        public HubList(IEnumerable<Hub> hubs)
        {
            Hubs = hubs;
        }

        public IEnumerable<Hub> Hubs { get; }
    }
}