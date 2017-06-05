using System;
using System.Text;
using SignalRTypingsCreator.Core.Typings.Models;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptHub
    {
        private readonly TypeScriptServerHub _serverHub;
        private readonly TypeScriptClientHub _clientHub;

        public TypeScriptHub(TypeScriptServerHub serverHub, TypeScriptClientHub clientHub)
        {
            _serverHub = serverHub;
            _clientHub = clientHub;
        }

        public string GetTypingsFileName()
        {
            return $"{_serverHub.GetHubName()}.d.ts";
        }

        public string GenerateClassDefinition()
        {
            StringBuilder stringBuilder = new StringBuilder();
            CreateHubInterface(stringBuilder);
            AddEmptyLine(stringBuilder);
            CreateSignalRInterface(stringBuilder);
            AddEmptyLine(stringBuilder);
            _serverHub.CreateHubServerInterface(stringBuilder);
            AddEmptyLine(stringBuilder);
            _clientHub.CreateHubClientInterface(stringBuilder);
            return stringBuilder.ToString();
        }

        public void AddModelsToCollection(TypeScriptModelList modelCollection)
        {
            _serverHub.AddModelsToCollection(modelCollection);
            _clientHub.AddModelsToCollection(modelCollection);
        }

        private void CreateHubInterface(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"interface {_serverHub.GetHubName()} {{");
            stringBuilder.AppendLine($"     server:{_serverHub.GetHubServerTypeName()},");
            stringBuilder.AppendLine($"     client:{_clientHub.GetHubClientTypeName()}");
            stringBuilder.AppendLine("}");
        }

        private void AddEmptyLine(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine();
        }

        private void CreateSignalRInterface(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("interface SignalR");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"     {GetInstanceName()}:{_serverHub.GetHubName()}");
            stringBuilder.AppendLine("}");
        }

        private string GetInstanceName()
        {
            var name = _serverHub.GetHubName();
            return Char.ToLowerInvariant(name[0]) + name.Substring(1);
        }
    }
}