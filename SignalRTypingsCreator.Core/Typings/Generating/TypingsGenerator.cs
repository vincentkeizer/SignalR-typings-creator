using System.Collections.Generic;
using System.Reflection;
using SignalRTypingsCreator.Core.Hubs;

namespace SignalRTypingsCreator.Core.Typings.Generating
{
    public class TypingsGenerator
    {
        public TypeScriptHubList Generate(HubList hubList)
        {
            var typeScriptClasslist = new List<TypeScriptHubClass>();
            foreach (var hub in hubList.Hubs)
            {
                var typeScriptClass = GenerateTyping(hub);
                typeScriptClasslist.Add(typeScriptClass);
            }

            return new TypeScriptHubList(typeScriptClasslist);
        }

        private TypeScriptHubClass GenerateTyping(Hub hub)
        {
            var typeScriptClass = new TypeScriptHubClass(hub.HubType);
            foreach (var method in hub.HubType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                if (method.DeclaringType == hub.HubType)
                {
                    var typeScriptMethod = new TypeScriptMethod(method);
                    typeScriptClass.AddMethod(typeScriptMethod);
                }
            }
            return typeScriptClass;
        }
    }
}