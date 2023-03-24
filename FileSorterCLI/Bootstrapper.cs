using FileSorterCLILib;
using Lamar;

namespace FileSorterCLI;

public static class Bootstrapper
{
    public static Container Bootstrap()
    {
        return new Container(c =>
            c.Scan(s =>
            {
                s.TheCallingAssembly();
                s.WithDefaultConventions();
                s.AssembliesFromApplicationBaseDirectory();
                s.AddAllTypesOf<IProcessor>();
            }));
    }
}