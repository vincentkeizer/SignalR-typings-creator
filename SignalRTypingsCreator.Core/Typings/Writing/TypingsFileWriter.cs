using System.Collections.Generic;
using System.IO;

namespace SignalRTypingsCreator.Core.Typings.Writing
{
    public class TypingsFileWriter
    {
        public void WriteFiles(string projectRootDir, IEnumerable<TypeScriptClass> typeScriptClasses)
        {
            var typingsDir = GetTypingsDirectory(projectRootDir);

            foreach (var typeScriptClass in typeScriptClasses)
            {
                File.WriteAllText(typingsDir + typeScriptClass.GetTypingsFileName(), typeScriptClass.GenerateClass());
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