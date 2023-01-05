using FileSorterCLILib;
using FileSorterCLILib.Models;
using Moq;

namespace FileSorterCLITests;

[TestClass]
public class SortProcessorTests
{
    private Mock<IFileTool> _mockFileTool;
    private SortProcessor _sut;

    [TestInitialize]
    public void Setup()
    {
        _mockFileTool = new Mock<IFileTool>();
        _sut = new SortProcessor(_mockFileTool.Object);
    }

    [TestMethod]
    public void It_should_call_directoryExists_on_the_fileTool_for_each_file_extension()
    {
        const string baseDirectory = @"C:\_Temp";
        var info1 = new MyFileInfo { Extension = "txt" };
        var info2 = new MyFileInfo { Extension = "png" };
    
        var fileInfos = new List<MyFileInfo> { info1, info2 };
        _mockFileTool.Setup(x => x.GetFileInfos(baseDirectory)).Returns(fileInfos);
    
        _sut.Process(baseDirectory);
    
        _mockFileTool.Verify(x => x.DirectoryExists(@"C:\_Temp\TXT"), Times.Once);
        _mockFileTool.Verify(x => x.DirectoryExists(@"C:\_Temp\PNG"), Times.Once);
    }

    [TestMethod]
    public void It_should_call_createDirectory_on_the_file_tool_for_each_file_extension()
    {
        const string baseDirectory = @"C:\_Temp";
        var info1 = new MyFileInfo { Extension = "txt" };
        var info2 = new MyFileInfo { Extension = "png" };
        var fileInfos = new List<MyFileInfo> { info1, info2 };
        _mockFileTool.Setup(x => x.GetFileInfos(baseDirectory)).Returns(fileInfos);
        _mockFileTool.Setup(x => x.DirectoryExists(@"C:\_Temp\TXT")).Returns(false);
        _mockFileTool.Setup(x => x.DirectoryExists(@"C:\_Temp\PNG")).Returns(false);

        _sut.Process(baseDirectory);

        _mockFileTool.Verify(x => x.CreateDirectory(@"C:\_Temp\TXT"), Times.Once);
        _mockFileTool.Verify(x => x.CreateDirectory(@"C:\_Temp\PNG"), Times.Once);
    }

    [TestMethod]
    public void It_should_not_call_createDirectory_on_the_file_tool_if_the_directory_already_exists()
    {
        const string baseDirectory = @"C:\_Temp";
        var info1 = new MyFileInfo { Extension = "txt" };
        var fileInfos = new List<MyFileInfo> { info1 };
        _mockFileTool.Setup(x => x.GetFileInfos(baseDirectory)).Returns(fileInfos);
    
        _mockFileTool.Setup(x => x.DirectoryExists(@"C:\_Temp\TXT")).Returns(true);
    
        _sut.Process(baseDirectory);
    
        _mockFileTool.Verify(x => x.CreateDirectory(@"C:\_Temp\TXT"), Times.Never);
    }
    
    [TestMethod]
    public void It_should_call_moveFile_on_the_file_tool_when_given_a_source_file_and_target_directory()
    {
        const string baseDirectory = @"C:\_Temp";
        var info1 = new MyFileInfo
        {
            DirectoryName = "fileDirectoryName",
            Extension = "txt"
        };
        var fileInfos = new List<MyFileInfo> { info1 };
        _mockFileTool.Setup(x => x.GetFileInfos(baseDirectory)).Returns(fileInfos);
    
        _mockFileTool.Setup(x => x.DirectoryExists(@"C:\_Temp\txt")).Returns(true);
    
        _sut.Process(baseDirectory);
    
        _mockFileTool.Verify(x => x.MoveFile("fileDirectoryName", @"C:\_Temp\TXT"), Times.Once());
    }

    [TestMethod]
    public void It_should_return_true_when_Handle_is_called_with_the_given_command_regardless_of_case()
    {
        var given = "Sort";
        var results = _sut.Handles(given);
        Assert.AreEqual(true, results);
        
        given = "sort";
        results = _sut.Handles(given);
        Assert.AreEqual(true, results);
        
        given = "SoRt";
        results = _sut.Handles(given);
        Assert.AreEqual(true, results);
    }
    
    [TestMethod]
    public void It_should_return_false_when_Handle_is_called_with_the_given_command_regardless_of_case()
    {
        var given = "notSort";
        var results = _sut.Handles(given);
        Assert.AreEqual(false, results);
    }
}