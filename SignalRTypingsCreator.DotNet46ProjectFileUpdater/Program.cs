using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using SignalRTypingsCreator.Core.Typings.Writing;
using SignalRTypingsCreator.DotNet46ProjectFileUpdater.HubTypings;

namespace SignalRTypingsCreator.DotNet46ProjectFileUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            var projectFileUpdater = new ProjectFiles.ProjectFileUpdater(args[0], args[1]);

            var relativePath = "Scripts\\typings\\signalrhubs\\";
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["TypingsPath"]))
            {
                relativePath = ConfigurationManager.AppSettings["TypingsPath"];
            }

            var hubTypingsResolver = new HubTypingsResolver();
            var typingFiles = hubTypingsResolver.ResolveAllHubTypings(relativePath);

            projectFileUpdater.Add(typingFiles);
        }
        
    }
}
