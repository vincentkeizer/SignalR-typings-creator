using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNet.SignalR.Hubs;
using SignalRTypingsCreator.Core.Hubs;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptClientHub
    {
        private readonly Type _clientHubType;

        public TypeScriptClientHub(Type clientHubType)
        {
            _clientHubType = clientHubType;
        }

        public void CreateHubClientInterface(StringBuilder stringBuilder)
        {
            if (_clientHubType == null)
            {
                return;
            }

            stringBuilder.AppendLine($"interface {GetHubName()}Client {{");
            foreach (var method in GetMethods())
            {
                stringBuilder.AppendLine($"     {method.GenerateServerMethodDefinition()}");
            }
            stringBuilder.AppendLine("}");
        }

        public IEnumerable<TypeScriptMethod> GetMethods()
        {
            if (_clientHubType == null)
            {
                return new List<TypeScriptMethod>();
            }

            var methods = new List<TypeScriptMethod>();
            foreach (var method in _clientHubType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                if (method.DeclaringType == _clientHubType)
                {
                    var typeScriptMethod = new TypeScriptMethod(method);
                    methods.Add(typeScriptMethod);
                }
            }
            return methods;
        }

        public string GetHubClientTypeName()
        {
            return _clientHubType == null
                                        ? "any"
                                        : $"{GetHubName()}Client";
        }

        private string GetHubName()
        {
            var hubType = _clientHubType.GetInterfaces().First(i => i.GetGenericTypeDefinition() == typeof(IHubClient<>)).GenericTypeArguments.First();

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