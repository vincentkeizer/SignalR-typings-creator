using System.Reflection;
using Microsoft.AspNet.SignalR.Hubs;
using TypingsCreator.Core.Naming;

namespace SignalRTypingsCreator.Core.Typings.Naming
{
    public class HubMethodNameResolver : ITypeScriptMethodNameResolver
    {
        public string GetMethodName(MethodInfo method)
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