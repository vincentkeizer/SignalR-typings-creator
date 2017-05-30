using System.Collections.Generic;
using System.Linq;

namespace SignalRTypingsCreator.Core.Typings.Writing
{
    public class ProjectFileUpdater
    {
        private readonly string _projectRootDir;
        private readonly string _projectFileFullPath;

        public ProjectFileUpdater(string projectRootDir, string projectFileFullPath)
        {
            _projectRootDir = projectRootDir;
            _projectFileFullPath = projectFileFullPath;
        }

        public void Add(IEnumerable<TypingsFile> files)
        {
            var updated = false;
            var project = new Microsoft.Build.Evaluation.Project(_projectFileFullPath);
            foreach (var file in files)
            {
                var relativePath = GetRelativePath(file);
                if (project.Items.FirstOrDefault(i => i.EvaluatedInclude == relativePath) == null)
                {
                    updated = true;
                    project.AddItem("TypeScriptCompile", relativePath);
                }
            }
            if (updated)
            {
                project.Save();
            }
        }

        private string GetRelativePath(TypingsFile file)
        {
            var relativePath = file.Path.Replace(_projectRootDir, "");
            while (relativePath.StartsWith("\\"))
            {
                relativePath = relativePath.Substring(1);
            }
            return relativePath;
        }
    }
}