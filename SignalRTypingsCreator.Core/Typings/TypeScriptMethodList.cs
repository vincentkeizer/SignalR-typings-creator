using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SignalRTypingsCreator.Core.Typings
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

        public void CreateTypeScriptMethods(StringBuilder stringBuilder)
        {
            var totalNumberOfMethods = _methods.Count;
            var i = 1;
            foreach (var method in _methods)
            {
                var lineSeparator = "";
                if (i < totalNumberOfMethods)
                {
                    lineSeparator = ",";
                }
                i++;

                stringBuilder.AppendLine($"     {method.GenerateServerMethodDefinition()}{lineSeparator}");
            }
        }

        public IEnumerable<TypeScriptModel> GetModels()
        {
            return _methods.SelectMany(m => m.GetModels());
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