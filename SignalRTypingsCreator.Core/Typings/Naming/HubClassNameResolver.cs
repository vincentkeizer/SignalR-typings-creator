using System;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRTypingsCreator.Core.Typings.Naming
{
    public class HubClassNameResolver
    {
        public string GetHubClassName(Type hubType)
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