using System;
using TypingsCreator.Core.Classes;

namespace SignalRTypingsCreator.Core.Models
{
    public class TypeScriptModelFactory : ITypeScriptClassFactory
    {
        public ITypeScriptClass Create(Type type)
        {
            return new TypeScriptModel(type);
        }
    }
}