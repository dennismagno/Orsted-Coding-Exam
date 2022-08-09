using Employee_App.Models;

namespace Employee_App.FileProcessor;

public interface IFileProcessor
{
	bool ProcessFile();
	IEnumerable<IEmployee> Data { get; }
	string Error { get; }
}