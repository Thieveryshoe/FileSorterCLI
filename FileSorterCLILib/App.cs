namespace FileSorterCLILib;

public interface IApp
{
    void Execute(string command, string directory);
}

public class App : IApp
{
    private readonly List<IProcessor> _processors;
    public App(List<IProcessor> processors)
    {
        _processors = processors;
    }
    
    public void Execute(string command, string directory)
    {
        var processor = GetProcessor(command);
        processor.Process(directory);
    }

    private IProcessor GetProcessor(string command)
    {
        return _processors.First(p => p.Handles(command));
    }
}