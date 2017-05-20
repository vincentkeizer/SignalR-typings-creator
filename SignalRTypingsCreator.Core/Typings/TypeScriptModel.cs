using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using SignalRTypingsCreator.Core.Typings.Types;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptModel : IEquatable<TypeScriptModel>
    {
        private readonly Type _modelType;
        private readonly TypeScriptTypeHandler _typeScriptTypeHandler;
        private readonly TypeScriptModelCreator _typeScriptModelCreator;

        public TypeScriptModel(Type modelType)
        {
            _modelType = modelType;
            _typeScriptTypeHandler = new TypeScriptTypeHandler();
            _typeScriptModelCreator = new TypeScriptModelCreator();
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

        public IEnumerable<TypeScriptModel> GetModels()
        {
            var models = new List<TypeScriptModel>();
            foreach (var property in GetProperties())
            {
                if (_typeScriptTypeHandler.IsUnknownType(property.PropertyType))
                {
                    var model = _typeScriptModelCreator.CreateModel(property);
                    var innerModels = model.GetModels();
                    models.AddRange(innerModels);
                    models.Add(model);
                }
            }
            return models;
        }

        public string GenerateModelDefinition()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"interface {_modelType.Name} {{");
            foreach (var property in GetProperties())
            {
                stringBuilder.AppendLine($"     {property.Name}:{_typeScriptTypeHandler.GetTypeScriptType(property.PropertyType)}");
            }
            stringBuilder.AppendLine("}");
            return stringBuilder.ToString();
        }

        private PropertyInfo[] GetProperties()
        {
            return _modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
    }
}