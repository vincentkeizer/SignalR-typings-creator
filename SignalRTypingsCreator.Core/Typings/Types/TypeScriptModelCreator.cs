using System.Collections.Generic;
using System.Reflection;

namespace SignalRTypingsCreator.Core.Typings.Types
{
    public class TypeScriptModelCreator
    {
        private readonly TypeScriptTypeHandler _typeScriptTypeHandler;

        public TypeScriptModelCreator()
        {
            _typeScriptTypeHandler = new TypeScriptTypeHandler();
        }

        public IEnumerable<TypeScriptModel> CreateModels(MethodInfo methodInfo)
        {
            var models = new List<TypeScriptModel>();
            if (_typeScriptTypeHandler.IsUnknownType(methodInfo.ReturnType))
            {
                models.Add(new TypeScriptModel(methodInfo.ReturnType));
            }
            var parameters = methodInfo.GetParameters();
            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                if (_typeScriptTypeHandler.IsUnknownType(parameter.ParameterType))
                {
                    models.Add(new TypeScriptModel(parameter.ParameterType));
                }
            }
            return models;
        }

        public TypeScriptModel CreateModel(PropertyInfo property)
        {
            if (_typeScriptTypeHandler.IsUnknownType(property.PropertyType))
            {
                return new TypeScriptModel(property.PropertyType);
            }
            return null;
        }
    }
}