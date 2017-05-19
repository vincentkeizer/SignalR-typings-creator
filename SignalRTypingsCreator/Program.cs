using SignalRTypingsCreator.Core;

namespace SignalRTypingsCreator
{
    public class Program
    {
        static int Main(string[] args)
        {
            var config = CreateSignalRTypingsCreatorConfigFromArgs(args);
            var typingsCreator = new Core.SignalRTypingsCreator();
            typingsCreator.Generate(config);

            return 0;
        }

        private static SignalRTypingsCreatorConfig CreateSignalRTypingsCreatorConfigFromArgs(string[] args)
        {
            var config = new SignalRTypingsCreatorConfig
            {
                AssemblyName = args[0],
                ProjectRootDir = args[1]
            };
            return config;
        }
    }
}
