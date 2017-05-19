using System;
using SignalRTypingsCreator.Core.Hubs.Finding;
using SignalRTypingsCreator.Core.Typings.Generating;
using SignalRTypingsCreator.Core.Typings.Writing;

namespace SignalRTypingsCreator.Core
{
    public class SignalRTypingsCreator
    {
        public void Generate(SignalRTypingsCreatorConfig config)
        {
            if (config == null)
            {
                throw new Exception("Config cannot be null");
            }

            var assembly = AppDomain.CurrentDomain.Load(config.AssemblyName);
            if (assembly == null)
            {
                throw new Exception("Assembly not found");
            }

            var hubFinder = new HubFinder();
            var hubs = hubFinder.FindHubs(assembly);

            var generator = new TypingsGenerator();
            var typeScriptClasses = generator.Generate(hubs);

            var writer = new TypingsFileWriter();
            writer.WriteFiles(config.ProjectRootDir, typeScriptClasses);

        }
    }
}
