namespace Employee_App.FileProcessor;
public interface IFileProcessorFactory
{
	IFileProcessor CreateFileProcessor(string filePath);
}