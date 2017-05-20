using System.Collections.Generic;
using System.Linq;

namespace SignalRTypingsCreator.Core.Typings
{
    public class TypeScriptHubList
    {
        private readonly IEnumerable<TypeScriptHubClass> _typeScriptHubClasses;

        public TypeScriptHubList(IEnumerable<TypeScriptHubClass> typeScriptHubClasses)
        {
            _typeScriptHubClasses = typeScriptHubClasses;
        }

        public IEnumerable<TypeScriptHubClass> GetTypeScriptHubClasses()
        {
            return _typeScriptHubClasses;
        }

        public IEnumerable<TypeScriptModel> GetTypeScriptModels()
        {
            var fullCollectionOfModels = new List<TypeScriptModel>();
            var hubModels = _typeScriptHubClasses.SelectMany(m => m.GetModels());
            fullCollectionOfModels.AddRange(hubModels);
            foreach (var model in hubModels)
            {
                fullCollectionOfModels.AddRange(model.GetModels());
            }
            return fullCollectionOfModels.Distinct();
        }
    }
}