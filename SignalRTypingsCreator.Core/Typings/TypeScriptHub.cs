using System;
using System.Text;
using TypingsCreator.Core.Classes;
using TypingsCreator.Core.Files;
using TypingsCreator.Core.Models;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptHub : IModelProvider, ITypeScriptFile, ITypeScriptClass
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

        protected bool Equals(TypeScriptHub other)
        {
            return Equals(_serverHub, other._serverHub) && Equals(_clientHub, other._clientHub);
        }

        public bool Equals(ITypeScriptClass other)
        {
            if (other is TypeScriptHub)
            {
                return Equals((TypeScriptHub)other);
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TypeScriptHub) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_serverHub != null ? _serverHub.GetHashCode() : 0) * 397) ^ (_clientHub != null ? _clientHub.GetHashCode() : 0);
            }
        }
    }
}