using System;
using System.Text;
using SignalRTypingsCreator.Core.Typings.Methods;
using SignalRTypingsCreator.Core.Typings.Models;
using SignalRTypingsCreator.Core.Typings.Naming;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptServerHub
    {
        private readonly Type _hubType;
        private readonly TypeScriptMethodList _methodList;
        private readonly HubClassNameResolver _hubClassNameResolver;

        public TypeScriptServerHub(Type hubType)
        {
            _hubType = hubType;
            _hubClassNameResolver = new HubClassNameResolver();
            _methodList = new TypeScriptMethodList(hubType);
        }

        public void CreateHubServerInterface(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"interface {GetHubServerTypeName()} {{");
            _methodList.GenerateMethodDefinitions(stringBuilder);
            stringBuilder.AppendLine("}");
        }

        public void AddModelsToCollection(TypeScriptModelList modelCollection)
        {
            _methodList.AddModelsToCollection(modelCollection);
        }

        public string GetHubName()
        {
            return _hubClassNameResolver.GetHubClassName(_hubType);
        }

        public string GetHubServerTypeName()
        {
            return $"{GetHubName()}Server";
        }
    }
}