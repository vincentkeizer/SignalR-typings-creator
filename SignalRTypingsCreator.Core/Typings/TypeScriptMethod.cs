using System;
using System.Reflection;
using System.Text;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptMethod
    {
        private readonly MethodInfo _method;

        public TypeScriptMethod(MethodInfo method)
        {
            this._method = method;
        }

        public string GenerateServerMethodDefinition()
        {
            StringBuilder stringBuilder = new StringBuilder();

            var name = GetName();
            stringBuilder.Append(name).Append("(");

            AddParameters(stringBuilder);
            
            stringBuilder.Append("):");
            stringBuilder.Append(GetTypeScriptType(_method.ReturnType));
            return stringBuilder.ToString();
        }
        
        private string GetName()
        {
            var methodName = _method.Name;
            var hubMethodNameAttribute = _method.GetCustomAttributes(typeof(HubMethodNameAttribute), false);
            if (hubMethodNameAttribute.Length > 0)
            {
                var hubMethodNameValue = (HubMethodNameAttribute)hubMethodNameAttribute.GetValue(0);
                methodName = hubMethodNameValue.MethodName;
            }
            return Char.ToLowerInvariant(methodName[0]) + methodName.Substring(1);
        }

        private void AddParameters(StringBuilder stringBuilder)
        {
            var parameters = _method.GetParameters();
            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                AddParameter(stringBuilder, parameter);
                if (i < parameters.Length - 1)
                {
                    stringBuilder.Append(", ");
                }
            }
        }

        private void AddParameter(StringBuilder stringBuilder, ParameterInfo parameter)
        {
            stringBuilder.Append(parameter.Name);
            stringBuilder.Append(":");
            stringBuilder.Append(GetTypeScriptType(parameter.ParameterType));
        }

        private string GetTypeScriptType(Type type)
        {
            switch (type.Name)
            {
                case "String":
                    return "string";
                case "Int":
                case "Int32":
                case "Int64":
                    return "number";
                case "Double":
                case "Float":
                    return "decimal";
                case "Void":
                    return "void";
                default:
                    return "any";
            }
        }
    }
}