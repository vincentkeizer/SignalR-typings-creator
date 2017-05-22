using System.Linq;
using System.Reflection;
using SignalRTypingsCreator.Core.Hubs.Creation;

namespace SignalRTypingsCreator.Core.Hubs.Finding
{
    public class HubFinder
    {
        public HubList FindHubs(Assembly assembly)
        {
            var hubFactory = new HubFactory();
            var hubs = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Microsoft.AspNet.SignalR.Hub))).Select(hubFactory.Create);
            return new HubList(hubs);
        }
    }
}
