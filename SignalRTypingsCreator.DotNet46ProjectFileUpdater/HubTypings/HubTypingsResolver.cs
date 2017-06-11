using System.Collections.Generic;
using System.IO;
using System.Linq;
using SignalRTypingsCreator.Core.Typings.Writing;

namespace SignalRTypingsCreator.DotNet46ProjectFileUpdater.HubTypings
{
    public class HubTypingsResolver
    {
        public IEnumerable<TypingsFile> ResolveAllHubTypings(string relativePath)
        {
            var files = Directory.GetFiles(relativePath);
            var typingsFiles = files.Where(file => file.EndsWith("d.ts")).Select(file => new TypingsFile(file));
            return typingsFiles;
        }
    }
}