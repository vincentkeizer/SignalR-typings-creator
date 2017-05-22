using System;

namespace SignalRTypingsCreator.Core.Typings.Creation
{
    public class TypeScriptHubClassFactory
    {
        public TypeScriptHubClass Create(Type type)
        {
            return new TypeScriptHubClass(type);
        }
    }
}