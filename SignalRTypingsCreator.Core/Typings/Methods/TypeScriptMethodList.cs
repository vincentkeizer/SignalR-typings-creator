using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using SignalRTypingsCreator.Core.Typings.Models;

namespace SignalRTypingsCreator.Core.Typings.Methods
{
    public class TypeScriptMethodList
    {
        private readonly Type _type;
        private IList<TypeScriptMethod> _methods;

        public TypeScriptMethodList(Type type)
        {
            _type = type;
            FindMethods();
        }

        public void GenerateMethodDefinitions(StringBuilder stringBuilder)
        {
            var totalNumberOfMethods = _methods.Count;
            var i = 1;
            foreach (var method in _methods)
            {
                var lineEnd = "";
                if (i < totalNumberOfMethods)
                {
                    lineEnd = ",";
                }
                i++;

                stringBuilder.AppendLine($"     {method.GenerateMethodDefinition()}{lineEnd}");
            }
        }

        public void AddModelsToCollection(TypeScriptModelList modelCollection)
        {
            foreach (var method in _methods)
            {
                method.AddModelsToCollection(modelCollection);
            }
        }

        private void FindMethods()
        {
            _methods = new List<TypeScriptMethod>();
            if (_type == null)
            {
                return;
            }

            foreach (var method in _type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
            {
                if (method.DeclaringType == _type)
                {
                    var typeScriptMethod = new TypeScriptMethod(method);
                    _methods.Add(typeScriptMethod);
                }
            }
        }
    }
}