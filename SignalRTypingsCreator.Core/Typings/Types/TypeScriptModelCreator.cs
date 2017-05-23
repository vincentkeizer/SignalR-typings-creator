using System;
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
            var returnTypeModel = CreateTypeScriptModel(methodInfo.ReturnType);
            if (returnTypeModel != null)
            {
                models.Add(returnTypeModel);
            }
            var parameters = methodInfo.GetParameters();
            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                var parameterModel = CreateTypeScriptModel(parameter.ParameterType);
                if (parameterModel != null)
                {
                    models.Add(parameterModel);
                }
            }
            return models;
        }

        public TypeScriptModel CreateModel(PropertyInfo property)
        {
            return CreateTypeScriptModel(property.PropertyType);
        }

        private TypeScriptModel CreateTypeScriptModel(Type type)
        {
            if (_typeScriptTypeHandler.IsUnknownType(type))
            {
                if (_typeScriptTypeHandler.IsCollection(type))
                {
                    var collectionType = _typeScriptTypeHandler.GetTypeFromCollection(type);
                    return new TypeScriptModel(collectionType);
                }
                return new TypeScriptModel(type);
            }
            
            return null;
        }
    }
}