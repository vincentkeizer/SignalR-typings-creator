using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptServerHub
    {
        private readonly Type _hubType;
        private readonly TypeScriptMethodList _methodList;

        public TypeScriptServerHub(Type hubType)
        {
            _hubType = hubType;
            _methodList = new TypeScriptMethodList(hubType);
        }

        public void CreateHubServerInterface(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"interface {GetHubServerTypeName()} {{");
            _methodList.CreateTypeScriptMethods(stringBuilder);
            stringBuilder.AppendLine("}");
        }

        public TypeScriptMethodList GetMethodList()
        {
            return _methodList;
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