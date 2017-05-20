using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNet.SignalR.Hubs;
using SignalRTypingsCreator.Core.Typings.Types;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptMethod
    {
        private readonly MethodInfo _method;
        private readonly TypeScriptTypeHandler _typeScriptTypeHandler;
        private readonly TypeScriptModelCreator _typeScriptModelCreator;

        public TypeScriptMethod(MethodInfo method)
        {
            this._method = method;
            _typeScriptModelCreator = new TypeScriptModelCreator();
            _typeScriptTypeHandler = new TypeScriptTypeHandler();

        }

        public string GenerateServerMethodDefinition()
        {
            StringBuilder stringBuilder = new StringBuilder();

            var name = GetName();
            stringBuilder.Append(name).Append("(");

            AddParameters(stringBuilder);
            
            stringBuilder.Append("):");
            stringBuilder.Append(_typeScriptTypeHandler.GetTypeScriptType(_method.ReturnType));
            return stringBuilder.ToString();
        }

        public IEnumerable<TypeScriptModel> GetModels()
        {
            return _typeScriptModelCreator.CreateModels(_method);
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
            stringBuilder.Append(_typeScriptTypeHandler.GetTypeScriptType(parameter.ParameterType));
        }
    }
}