namespace SignalRTypingsCreator
{
    public class Program
    {
        static int Main(string[] args)
        {
            var typingsCreator = new Core.SignalRTypingsCreator();
            typingsCreator.Generate(args[0], args[1]);

            return 0;
        }
    }
}
