using FileSorterCLILib.Models;

namespace FileSorterCLILib;

public interface IFileTool
{
    List<MyFileInfo> GetFileInfos(string directory);
    void CreateDirectory(string directory);
    bool DirectoryExists(string directory);
    void MoveFile(string file, string targetDirectory);
}

public class FileTool : IFileTool
{
    public List<MyFileInfo> GetFileInfos(string directory)
    {
        throw new NotImplementedException();
    }

    public void CreateDirectory(string directory)
    {
        throw new NotImplementedException();
    }

    public bool DirectoryExists(string directory)
    {
        throw new NotImplementedException();
    }

    public void MoveFile(string file, string targetDirectory)
    {
        throw new NotImplementedException();
    }
}