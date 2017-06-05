using System;
using System.Reflection;
using SignalRTypingsCreator.Core.Typings.TypeConversion;

namespace SignalRTypingsCreator.Core.Typings.Models
{
    public class TypeScriptModelCreator
    {
        private readonly TypeScriptTypeHandler _typeScriptTypeHandler;

        public TypeScriptModelCreator()
        {
            _typeScriptTypeHandler = new TypeScriptTypeHandler();
        }

        public TypeScriptModelList CreateModels(MethodInfo methodInfo)
        {
            var modelList = new TypeScriptModelList();

            var returnTypeModel = CreateTypeScriptModel(methodInfo.ReturnType);
            modelList.Add(returnTypeModel);
            
            var parameters = methodInfo.GetParameters();
            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                var parameterModel = CreateTypeScriptModel(parameter.ParameterType);
                modelList.Add(parameterModel);
            }

            return modelList;
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