using System.Collections.Generic;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptHubList
    {
        public TypeScriptHubList(IEnumerable<TypeScriptHubClass> typeScriptHubClasses)
        {
            TypeScriptHubClasses = typeScriptHubClasses;
        }

        public IEnumerable<TypeScriptHubClass> TypeScriptHubClasses { get; }
    }
}