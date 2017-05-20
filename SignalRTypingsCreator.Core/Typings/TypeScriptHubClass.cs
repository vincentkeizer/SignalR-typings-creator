using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptHubClass
    {
        private readonly Type _hubType;
        private readonly IList<TypeScriptMethod> _methods;

        public TypeScriptHubClass(Type hubType)
        {
            _methods = new List<TypeScriptMethod>();
            _hubType = hubType;
        }

        public void AddMethod(TypeScriptMethod method)
        {
            _methods.Add(method);
        }

        public string GetTypingsFileName()
        {
            return $"{GetHubName()}.d.ts";
        }

        private string GetHubName()
        {
            var hubNameattribute = _hubType.GetCustomAttributes(typeof(HubNameAttribute), false);
            if (hubNameattribute.Length > 0)
            {
                var hubNameAttributeValue = (HubNameAttribute)hubNameattribute.GetValue(0);
                return hubNameAttributeValue.HubName;
            }
            return _hubType.Name;
        }

        public string GenerateClassDefinition()
        {
            StringBuilder stringBuilder = new StringBuilder();
            CreateHubInterface(stringBuilder);
            AddEmptyLine(stringBuilder);
            CreateHubServerInterface(stringBuilder);
            AddEmptyLine(stringBuilder);
            CreateSignalRInterface(stringBuilder);
            return stringBuilder.ToString();
        }

        public IEnumerable<TypeScriptModel> GetModels()
        {
            var typeScriptModels = _methods.SelectMany(m => m.GetModels());
            return typeScriptModels;
        }

        private void CreateHubInterface(StringBuilder stringBuilder)
        {
            var name = GetHubName();
            stringBuilder.AppendLine($"interface {name} {{");
            stringBuilder.AppendLine($"     server:{name}Server");
            stringBuilder.AppendLine($"     client:any");
            stringBuilder.AppendLine("}");
        }

        private void AddEmptyLine(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine();
        }

        private void CreateHubServerInterface(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"interface {GetHubName()}Server {{");
            foreach (var method in _methods)
            {
                stringBuilder.AppendLine($"     {method.GenerateServerMethodDefinition()}");
            }
            stringBuilder.AppendLine("}");
        }

        private void CreateSignalRInterface(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine("interface SignalR");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine($"     {GetInstanceName()}:{GetHubName()}");
            stringBuilder.AppendLine("}");
        }

        private string GetInstanceName()
        {
            var name = GetHubName();
            return Char.ToLowerInvariant(name[0]) + name.Substring(1);
        }
    }
}