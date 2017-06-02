using System;
using System.Collections.Generic;
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
            var hubTypes = FindServerHub(assembly);

            var hubsList = new List<Hub>();
            foreach (var hubType in hubTypes)
            {
                var hubClientType = GetClientHub(assembly, hubType);
                var hub = hubFactory.Create(hubType, hubClientType);

                hubsList.Add(hub);
            }
            
            return new HubList(hubsList);
        }

        private IEnumerable<Type> FindServerHub(Assembly assembly)
        {
            return assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Microsoft.AspNet.SignalR.Hub)));
        }

        private Type GetClientHub(Assembly assembly, Type hubType)
        {
            return assembly.GetTypes().FirstOrDefault(t => t.IsInterface 
                                                           && t.GetInterfaces().Any(i => i.IsGenericType 
                                                                                         && i.GetGenericTypeDefinition() == typeof(IHubClient<>)
                                                                                         && i.GenericTypeArguments.First() == hubType));
        }
    }
}
