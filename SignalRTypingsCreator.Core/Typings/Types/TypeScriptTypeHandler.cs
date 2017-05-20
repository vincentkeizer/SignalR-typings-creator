using System;

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
                case "Float":
                    return "decimal";
                case "Void":
                    return "void";
                default:
                    if (IsModel(type))
                    {
                        return GetTypeName(type);
                    }
                    return "any";
            }
        }

        public bool IsUnknownType(Type type)
        {
            return IsModel(type) && GetTypeScriptType(type) == GetTypeName(type);
        }

        private string GetTypeName(Type type)
        {
            return type.Name;
        }

        private bool IsModel(Type type)
        {
            return !type.IsValueType;
        }
    }
}