using System.Reflection;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRTypingsCreator.Core.Typings.Naming
{
    public class HubMethodNameResolver
    {
        public string GetHubMethodName(MethodInfo method)
        {
            var methodName = method.Name;
            var hubMethodNameAttribute = method.GetCustomAttributes(typeof(HubMethodNameAttribute), false);
            if (hubMethodNameAttribute.Length > 0)
            {
                var hubMethodNameValue = (HubMethodNameAttribute)hubMethodNameAttribute.GetValue(0);
                methodName = hubMethodNameValue.MethodName;
            }

            return methodName;
        }
    }
}