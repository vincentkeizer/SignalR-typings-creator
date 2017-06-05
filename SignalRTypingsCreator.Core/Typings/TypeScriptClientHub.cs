using System;
using System.Linq;
using System.Text;
using SignalRTypingsCreator.Core.Hubs;
using SignalRTypingsCreator.Core.Typings.Methods;
using SignalRTypingsCreator.Core.Typings.Models;
using SignalRTypingsCreator.Core.Typings.Naming;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptClientHub
    {
        private readonly Type _clientHubType;
        private readonly TypeScriptMethodList _methodList;
        private readonly HubClassNameResolver _hubClassNameResolver;

        public TypeScriptClientHub(Type clientHubType)
        {
            _clientHubType = clientHubType;
            _methodList = new TypeScriptMethodList(_clientHubType);
            _hubClassNameResolver = new HubClassNameResolver();
        }

        public void CreateHubClientInterface(StringBuilder stringBuilder)
        {
            if (_clientHubType == null)
            {
                return;
            }

            stringBuilder.AppendLine($"interface {GetHubName()}Client {{");
            _methodList.GenerateMethodDefinitions(stringBuilder);
            stringBuilder.AppendLine("}");
        }

        public void AddModelsToCollection(TypeScriptModelList modelCollection)
        {
            _methodList.AddModelsToCollection(modelCollection);
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

            return _hubClassNameResolver.GetHubClassName(hubType);
        }
    }
}