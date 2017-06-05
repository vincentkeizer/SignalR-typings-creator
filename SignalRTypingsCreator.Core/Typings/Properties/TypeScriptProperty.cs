using System.Reflection;
using SignalRTypingsCreator.Core.Typings.Models;
using SignalRTypingsCreator.Core.Typings.TypeConversion;

namespace SignalRTypingsCreator.Core.Typings.Properties
{
    public class TypeScriptProperty
    {
        private readonly PropertyInfo _property;
        private readonly TypeScriptTypeHandler _typeScriptTypeHandler;
        private readonly TypeScriptModelCreator _typeScriptModelCreator;

        public TypeScriptProperty(PropertyInfo property)
        {
            _property = property;
            _typeScriptTypeHandler = new TypeScriptTypeHandler();
            _typeScriptModelCreator = new TypeScriptModelCreator();
        }

        public void AddModelsToCollection(TypeScriptModelList modelCollection)
        {
            var model =_typeScriptModelCreator.CreateModel(_property);
            modelCollection.Add(model);
        }

        public string GeneratePropertyDefinition()
        {
            return $"     {_property.Name}:{_typeScriptTypeHandler.GetTypeScriptType(_property.PropertyType)}";
        }
    }
}