namespace FileSorterCLILib.Models;

public class FileInfoWrapper
{
    private readonly FileInfo _wrapped;

    public FileInfoWrapper(string fileName)
    {
        _wrapped = new FileInfo(fileName);
    }

    public FileInfoWrapper(FileInfo file)
    {
        _wrapped = file;
    }

    public string Directory => _wrapped.Directory.Name;
    public string DirectoryName => _wrapped.DirectoryName;
    public string Extension => _wrapped.Extension;
    public string Name => _wrapped.Name;
}