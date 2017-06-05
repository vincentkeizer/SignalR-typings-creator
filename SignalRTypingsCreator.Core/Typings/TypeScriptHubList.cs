using System.Collections.Generic;
using SignalRTypingsCreator.Core.Typings.Models;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptHubList
    {
        private readonly IEnumerable<TypeScriptHub> _typeScriptHubClasses;

        public TypeScriptHubList(IEnumerable<TypeScriptHub> typeScriptHubClasses)
        {
            _typeScriptHubClasses = typeScriptHubClasses;
        }

        public IEnumerable<TypeScriptHub> GetTypeScriptHubClasses()
        {
            return _typeScriptHubClasses;
        }

        public TypeScriptModelList GetTypeScriptModels()
        {
            var modelList = new TypeScriptModelList();
            foreach (var typeScriptHub in _typeScriptHubClasses)
            {
                typeScriptHub.AddModelsToCollection(modelList);
            }
            return modelList;
        }
    }
}