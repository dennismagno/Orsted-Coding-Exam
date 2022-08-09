using Employee_App.FileProcessor;

namespace FileProcessorApiTests;

public class FileProcessorTests
{
    private IFileProcessorFactory _fileProcessorFactory;
    private string _basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

    public FileProcessorTests()
    {
        if(_fileProcessorFactory == null)
        {
            _fileProcessorFactory = new FileProcessorFactory();
        }
    }

    [Fact]
    public void Should_Process_File_With_Correct_Type_And_Format()
    {
        // Arrange
        int dataCount = 3;
        string testFile = Path.Combine(_basePath, "TestFile", "ValidEmployeeFile.xlsx");
        IFileProcessor fileProcessor = _fileProcessorFactory.CreateFileProcessor(testFile);

        // Act
        bool result = fileProcessor.ProcessFile();

        //Assert
        Assert.True(result, "The file should be processed successfully.");
        Assert.True(dataCount == fileProcessor.Data.Count(), $"The total processed date should be {result}");
    }

     [Fact]
    public void Process_File_With_Invalid_Format_Return_Error()
    {
        // Arrange
        string errorMessage = "The file provided is not on a correct format.";
        string testFile = Path.Combine(_basePath, "TestFile", "InvalidHeaderEmployeeFile.xlsx");
        IFileProcessor fileProcessor = _fileProcessorFactory.CreateFileProcessor(testFile);

        // Act
        bool result = fileProcessor.ProcessFile();

        // Assert
        Assert.False(result, "The file should be not processed.");
        Assert.True(errorMessage == fileProcessor.Error, $"Should raise error {errorMessage}");
    }

     [Fact]
    public void Process_Unsupported_File_Format_Return_Error()
    {
        // Arrange
        string errorMessage = "The file that you provided is not supported.";
        string testFile = Path.Combine(_basePath, "TestFile", "Unsupported.txt");
        IFileProcessor fileProcessor = _fileProcessorFactory.CreateFileProcessor(testFile);

        // Act
        bool result = fileProcessor.ProcessFile();

        // Assert
        Assert.False(result, "The file should be not processed.");
        Assert.True(errorMessage == fileProcessor.Error, $"Should raise error {errorMessage}");
    }
}