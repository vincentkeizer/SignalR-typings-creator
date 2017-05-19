using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SignalRTypingsCreator.Core.Hubs.Finding
{
    public class HubFinder
    {
        public IEnumerable<Hub> FindHubs(Assembly assembly)
        {
            var hubs = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Microsoft.AspNet.SignalR.Hub))).Select(type => new Hub { HubType = type});
            return hubs;
        }
    }
}
