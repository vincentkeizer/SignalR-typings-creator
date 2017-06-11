namespace SignalRTypingsCreator.Core.Typings.Writing
{
    public class TypingsFile
    {
        public TypingsFile(string path)
        {
            Path = path;
        }

        public string Path { get;  }
    }
}