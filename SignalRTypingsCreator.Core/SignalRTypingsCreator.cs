using System;
using SignalRTypingsCreator.Core.Hubs.Finding;
using SignalRTypingsCreator.Core.Typings.Generating;
using SignalRTypingsCreator.Core.Typings.Writing;

namespace SignalRTypingsCreator.Core
{
    public class SignalRTypingsCreator
    {
        public void Generate(string assemblyName, string projectRootDir)
        {
            var assembly = AppDomain.CurrentDomain.Load(assemblyName);
            if (assembly != null)
            {
                var hubFinder = new HubFinder();
                var hubs = hubFinder.FindHubs(assembly);

                var generator = new TypingsGenerator();
                var typeScriptClasses = generator.Generate(hubs);

                var writer = new TypingsFileWriter();
                writer.WriteFiles(projectRootDir, typeScriptClasses);
            }
        }
    }
}
