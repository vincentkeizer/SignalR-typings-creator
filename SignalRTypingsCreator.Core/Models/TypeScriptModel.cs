using System;
using System.Text;
using TypingsCreator.Core.Classes;
using TypingsCreator.Core.Models;
using TypingsCreator.Core.TypeScriptProperties;
using TypingsCreator.Core.TypeScriptProperties.Naming;

namespace SignalRTypingsCreator.Core.Models
{
    public class TypeScriptModel : ITypeScriptClass
    {
        private readonly Type _modelType;
        private readonly TypeScriptPropertyList _typeScriptPropertyList;

        public TypeScriptModel(Type modelType)
        {
            _modelType = modelType;
            _typeScriptPropertyList = new TypeScriptPropertyList(modelType, new TypeScriptPropertyNameResolver(), new TypeScriptModelFactory());
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
        
        public string GenerateClassDefinition()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"interface {_modelType.Name} {{");

            _typeScriptPropertyList.GeneratePropertyDefinitions(stringBuilder);
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        public bool Equals(ITypeScriptClass other)
        {
            if (other is TypeScriptModel)
            {
                return Equals((TypeScriptModel)other);
            }
            return false;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TypeScriptModel)obj);
        }

        public static bool operator ==(TypeScriptModel obj1, TypeScriptModel obj2)
        {
            return obj1.Equals(obj2);
        }
        public static bool operator !=(TypeScriptModel obj1, TypeScriptModel obj2)
        {
            return !obj1.Equals(obj2);
        }
    }
}