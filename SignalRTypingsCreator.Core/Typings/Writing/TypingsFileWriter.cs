using System.IO;

namespace SignalRTypingsCreator.Core.Typings.Writing
{
    public class TypingsFileWriter
    {
        public void WriteFiles(string projectRootDir, TypeScriptHubList typeScriptHubList)
        {
            var typingsDir = GetTypingsDirectory(projectRootDir);

            foreach (var typeScriptClass in typeScriptHubList.TypeScriptHubClasses)
            {
                File.WriteAllText(typingsDir + typeScriptClass.GetTypingsFileName(), typeScriptClass.GenerateClassDefinition());
            }
        }

        private string GetTypingsDirectory(string projectRootDir)
        {
            EnsureDirectory($"{projectRootDir}\\Scripts");
            EnsureDirectory($"{projectRootDir}\\Scripts\\typings");
            return $"{projectRootDir}\\Scripts\\typings\\";
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