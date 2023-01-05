namespace FileSorterCLILib;

public interface IProcessor
{
    bool Handles(string command);
    void Process(string directory);
}