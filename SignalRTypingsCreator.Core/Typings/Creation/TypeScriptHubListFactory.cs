using System.Collections.Generic;
using SignalRTypingsCreator.Core.Hubs;

namespace SignalRTypingsCreator.Core.Typings.Creation
{
    public class TypeScriptHubListFactory
    {
        public TypeScriptHubList Create(HubList hubList)
        {
            var typeScriptClasslist = new List<TypeScriptHubClass>();
            var typeScriptHubClassFactory = new TypeScriptHubClassFactory();
            foreach (var hub in hubList.Hubs)
            {
                var typeScriptClass = typeScriptHubClassFactory.Create(hub.HubType);
                typeScriptClasslist.Add(typeScriptClass);
            }

            return new TypeScriptHubList(typeScriptClasslist);
        }
    }
}