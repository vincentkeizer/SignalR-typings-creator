using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptServerHub
    {
        private readonly Type _hubType;
        
        public TypeScriptServerHub(Type hubType)
        {
            _hubType = hubType;
        }

        public void CreateHubServerInterface(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"interface {GetHubName()}Server {{");
            foreach (var method in GetMethods())
            {
                stringBuilder.AppendLine($"     {method.GenerateServerMethodDefinition()}");
            }
            stringBuilder.AppendLine("}");
        }

        public IEnumerable<TypeScriptMethod> GetMethods()
        {
            var methods = new List<TypeScriptMethod>();
            foreach (var method in _hubType.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                if (method.DeclaringType == _hubType)
                {
                    var typeScriptMethod = new TypeScriptMethod(method);
                    methods.Add(typeScriptMethod);
                }
            }
            return methods;
        }

        public string GetHubName()
        {
            var hubNameattribute = _hubType.GetCustomAttributes(typeof(HubNameAttribute), false);
            if (hubNameattribute.Length > 0)
            {
                var hubNameAttributeValue = (HubNameAttribute) hubNameattribute.GetValue(0);
                return hubNameAttributeValue.HubName;
            }

            return _hubType.Name;
        }

        public string GetHubServerTypeName()
        {
            return $"{GetHubName()}Server";
        }
    }
}