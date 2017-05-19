using System.Collections.Generic;
using System.Reflection;
using SignalRTypingsCreator.Core.Hubs;

namespace SignalRTypingsCreator.Core.Typings.Generating
{
    public class TypingsGenerator
    {
        public IEnumerable<TypeScriptClass> Generate(IEnumerable<Hub> hubs)
        {
            var typeScriptClasslist = new List<TypeScriptClass>();
            foreach (var hub in hubs)
            {
                var typeScriptClass = GenerateTyping(hub);
                typeScriptClasslist.Add(typeScriptClass);
            }
            return typeScriptClasslist;
        }

        private TypeScriptClass GenerateTyping(Hub hub)
        {
            var typeScriptClass = new TypeScriptClass(hub.HubType);
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