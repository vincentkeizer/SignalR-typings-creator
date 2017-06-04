using System;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR.Hubs;
using SignalRTypingsCreator.Core.Hubs;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptClientHub
    {
        private readonly Type _clientHubType;
        private readonly TypeScriptMethodList _methodList;

        public TypeScriptClientHub(Type clientHubType)
        {
            _clientHubType = clientHubType;
            _methodList = new TypeScriptMethodList(_clientHubType);
        }

        public void CreateHubClientInterface(StringBuilder stringBuilder)
        {
            if (_clientHubType == null)
            {
                return;
            }

            stringBuilder.AppendLine($"interface {GetHubName()}Client {{");
            _methodList.CreateTypeScriptMethods(stringBuilder);
            stringBuilder.AppendLine("}");
        }

        public TypeScriptMethodList GetMethodList()
        {
            return _methodList;
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