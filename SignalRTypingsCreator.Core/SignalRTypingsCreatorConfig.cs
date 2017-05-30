namespace SignalRTypingsCreator.Core
{
    public class SignalRTypingsCreatorConfig
    {
        public string AssemblyName { get; set; }
        public string ProjectRootDir { get; set; }
        public string ProjectFileFullPath { get; set; }
        public string RelativeOutputDir { get; set; } = "Scripts\\typings\\signalrhubs\\";
    }
}