using System;

namespace SignalRTypingsCreator.Core.Typings.Creation
{
    public class TypeScriptHubClassFactory
    {
        public TypeScriptHub Create(Type hubType, Type hubClientType)
        {
            var serverHub = new TypeScriptServerHub(hubType);
            var clientHub = new TypeScriptClientHub(hubClientType);

            return new TypeScriptHub(serverHub, clientHub);
        }
    }
}