using System;
using System.Text;
using SignalRTypingsCreator.Core.Typings.Naming;
using TypingsCreator.Core.Methods;
using TypingsCreator.Core.Models;
using TypingsCreator.Core.Naming;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptServerHub : IModelProvider
    {
        private readonly Type _hubType;
        private readonly TypeScriptMethodList _methodList;
        private readonly ITypeScriptClassNameResolver _hubClassNameResolver;

        public TypeScriptServerHub(Type hubType)
        {
            _hubType = hubType;
            _hubClassNameResolver = new HubClassNameResolver();
            _methodList = new TypeScriptMethodList(hubType, new HubMethodNameResolver());
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
            return _hubClassNameResolver.GetClassName(_hubType);
        }

        public string GetHubServerTypeName()
        {
            return $"{GetHubName()}Server";
        }
    }
}