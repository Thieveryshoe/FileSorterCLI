using FileSorterCLILib.Models;

namespace FileSorterCLILib;

public class SortProcessor : IProcessor
{
    // TODO need to create the nuget package in order to do it this way.  
    private readonly IFileTool _fileTool;
    private static string CanHandle => "Sort";

    public SortProcessor(IFileTool fileTool)
    {
        _fileTool = fileTool;
    }

    public bool Handles(string command)
    {
        return string.Equals(command, CanHandle, StringComparison.OrdinalIgnoreCase);
    }

    public void Process(string directory)
    {
        var fileInfos = _fileTool.GetFileInfos(directory);
        
        // TODO v2 look into returning a dictionary and looping through each key, use benchmark nuget pkg to determine which is best way
        foreach (var fileInfo in fileInfos)
        {
            var targetDirectory = BuildTargetDirectory(directory, fileInfo);
            if (!_fileTool.DirectoryExists(targetDirectory))
                _fileTool.CreateDirectory(targetDirectory);
        
            _fileTool.MoveFile(fileInfo.DirectoryName, targetDirectory);
        }
    }

    private static string BuildTargetDirectory(string directory, MyFileInfo fileInfo)
    {
        var extension = fileInfo.Extension.ToUpper();
        var targetDir = $@"{directory}\{extension}";
        return targetDir;
    }
}

public interface IFileTool
{
    bool DirectoryExists(string targetDirectory);
    void MoveFile(string directoryName, string targetDirectory);
    IEnumerable<MyFileInfo> GetFileInfos(string directory);
    void CreateDirectory(string targetDirectory);
}