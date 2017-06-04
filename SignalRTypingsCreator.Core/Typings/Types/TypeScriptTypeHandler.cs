using System;
using System.Collections;

namespace SignalRTypingsCreator.Core.Typings.Types
{
    public class TypeScriptTypeHandler
    {
        public string GetTypeScriptType(Type type)
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
                case "Single":
                    return "decimal";
                case "Boolean":
                    return "boolean";
                case "Void":
                    return "void";
                default:
                    if (IsCollection(type))
                    {
                        var collectionType = GetCollectionType(type);
                        return $"{collectionType}[]";
                    }
                    if (IsModel(type))
                    {
                        return GetTypeName(type);
                    }
                    return "any";
            }
        }

        public bool IsUnknownType(Type type)
        {
            return IsCollection(type) || (IsModel(type) && GetTypeScriptType(type) == GetTypeName(type));
        }

        public bool IsCollection(Type type)
        {
            return type.IsArray || type.IsGenericType && typeof(IEnumerable).IsAssignableFrom(type);
        }

        public Type GetTypeFromCollection(Type type)
        {
            if (IsCollection(type))
            {
                if (type.IsArray)
                {
                    var arrayType = type.GetElementType();
                    return arrayType;
                }
                if (type.IsGenericType)
                {
                    var genericType = type.GetGenericArguments()[0];
                    return genericType;
                }
            }
            return null;
        }

        private string GetCollectionType(Type type)
        {
            var collectionType = GetTypeFromCollection(type);
            if (collectionType != null)
            {
                return GetTypeScriptType(collectionType);
            }
            return null;
        }

        private string GetTypeName(Type type)
        {
            return type.Name;
        }

        private bool IsModel(Type type)
        {
            return !type.IsValueType && !(type.IsGenericType && !IsCollection(type));
        }
    }
}