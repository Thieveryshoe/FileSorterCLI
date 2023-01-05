using Lamar;

namespace FileSorterCLI;

public class Bootstrapper
{
    public static Container Bootstrap()
    {
        //this is for default and "nothing special"
        return new Container(c =>
            c.Scan(s =>
            {
                s.TheCallingAssembly();
                s.WithDefaultConventions();
                s.AssembliesFromApplicationBaseDirectory();
            }));
    }
}