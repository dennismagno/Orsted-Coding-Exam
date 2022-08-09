namespace Employee_App.FileProcessor;

public class FileProcessorFactory : IFileProcessorFactory
{
	public IFileProcessor CreateFileProcessor(string filePath)
	{
        IFileProcessor fileProcessor = new UnsupportedFileProcessor();

		if (filePath != null)
		{
			string extension = Path.GetExtension(filePath);
			switch (extension.ToLower())
			{
				case ".xlsx":
					fileProcessor = new ExcelFileProcessor(filePath);
                    break;
				default:
					fileProcessor = new UnsupportedFileProcessor();
                    break;
			}
		}

		return fileProcessor;
	}
}