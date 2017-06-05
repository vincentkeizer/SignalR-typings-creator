using System;
using System.Text;
using SignalRTypingsCreator.Core.Typings.Properties;

namespace SignalRTypingsCreator.Core.Typings.Models
{
    public class TypeScriptModel : IEquatable<TypeScriptModel>
    {
        private readonly Type _modelType;
        private readonly TypeScriptPropertyList _typeScriptPropertyList;

        public TypeScriptModel(Type modelType)
        {
            _modelType = modelType;
            _typeScriptPropertyList = new TypeScriptPropertyList(modelType);
        }

        public bool Equals(TypeScriptModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return _modelType.FullName == other._modelType.FullName;
        }

        public override int GetHashCode()
        {
            return (_modelType != null ? _modelType.GetHashCode() : 0);
        }

        public string GetTypingsFileName()
        {
            return $"{_modelType.Name}.d.ts";
        }

        public void AddModelsToCollection(TypeScriptModelList modelCollection)
        {
            _typeScriptPropertyList.AddModelsToCollection(modelCollection);
        }

        public string GenerateModelDefinition()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"interface {_modelType.Name} {{");

            _typeScriptPropertyList.GeneratePropertyDefinitions(stringBuilder);

            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }
    }
}