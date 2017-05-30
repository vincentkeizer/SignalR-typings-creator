using System.Collections.Generic;
using System.IO;

namespace SignalRTypingsCreator.Core.Typings.Writing
{
    public class TypingsFileWriter
    {
        public IEnumerable<TypingsFile> WriteFiles(string projectRootDir, string relativeOutputDir, TypeScriptHubList typeScriptHubList)
        {
            var typingsFiles = new List<TypingsFile>();

            var typingsDir = GetTypingsDirectory(projectRootDir, relativeOutputDir);

            foreach (var typeScriptClass in typeScriptHubList.GetTypeScriptHubClasses())
            {
                var fullPath = typingsDir + typeScriptClass.GetTypingsFileName();
                File.WriteAllText(fullPath, typeScriptClass.GenerateClassDefinition());

                var typingsFile = new TypingsFile(fullPath);
                typingsFiles.Add(typingsFile);
            }
            foreach (var typeScriptModel in typeScriptHubList.GetTypeScriptModels())
            {
                var fullPath = typingsDir + typeScriptModel.GetTypingsFileName();
                File.WriteAllText(fullPath, typeScriptModel.GenerateModelDefinition());

                var typingsFile = new TypingsFile(fullPath);
                typingsFiles.Add(typingsFile);
            }

            return typingsFiles;
        }

        private string GetTypingsDirectory(string projectRootDir, string relativeOutputDir)
        {
            var fullDir = $"{projectRootDir}\\{relativeOutputDir}";
            EnsureDirectory(fullDir);
            return fullDir;
        }

        private void EnsureDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}