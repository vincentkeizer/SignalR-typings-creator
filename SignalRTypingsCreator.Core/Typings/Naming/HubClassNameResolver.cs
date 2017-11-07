using System;
using Microsoft.AspNet.SignalR.Hubs;
using TypingsCreator.Core.Naming;

namespace SignalRTypingsCreator.Core.Typings.Naming
{
    public class HubClassNameResolver : ITypeScriptClassNameResolver
    {
        public string GetClassName(Type hubType)
        {
            var hubNameattribute = hubType.GetCustomAttributes(typeof(HubNameAttribute), false);
            if (hubNameattribute.Length > 0)
            {
                var hubNameAttributeValue = (HubNameAttribute)hubNameattribute.GetValue(0);
                return hubNameAttributeValue.HubName;
            }

            return hubType.Name;
        }
    }
}