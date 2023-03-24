using FileTool;

namespace FileSorterCLILib;

public class SortProcessor : IProcessor
{
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

            var a = $@"{fileInfo.DirectoryName}\{fileInfo.Name}";
            var b = $@"{targetDirectory}\{fileInfo.Name}";
            _fileTool.MoveFile(a, b);
            //_fileTool.MoveFile(fileInfo.FullName, targetDirectory);

        }
    }

    private static string BuildTargetDirectory(string directory, MyFileInfo fileInfo)
    {
        var extension = fileInfo.Extension.Substring(1);
        var targetDir = $@"{directory}\{extension}";
        return targetDir;
    }
}