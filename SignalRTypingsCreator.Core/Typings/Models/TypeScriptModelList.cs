using System.Collections.Generic;

namespace SignalRTypingsCreator.Core.Typings.Models
{
    public class TypeScriptModelList
    {
        private readonly IList<TypeScriptModel> _models;

        public TypeScriptModelList()
        {
            _models = new List<TypeScriptModel>();    
        }

        public void Add(TypeScriptModelList typeScriptModelList)
        {
            var models = typeScriptModelList.GetModels();
            foreach (var model in models)
            {
                Add(model);
            }
         }

        public void Add(TypeScriptModel model)
        {
            if (model == null) { return; }

            if (!_models.Contains(model))
            {
                _models.Add(model);

                model.AddModelsToCollection(this);
            }
        }

        public IEnumerable<TypeScriptModel> GetModels()
        {
            return _models;
        }
    }
}