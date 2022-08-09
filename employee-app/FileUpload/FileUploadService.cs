namespace Employee_App.FileUpload;
public class FileUploadService : IFileUploadService
{
    string _filePath = string.Empty;
    public string FilePath {
        get => _filePath;
    }
    public async Task<bool> UploadFile(IFormFile file)
    {
        try
        {
            if (file.Length > 0)
            {
                string dirPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                _filePath = Path.Combine(dirPath, file.FileName);

                using (var fileStream = new FileStream(_filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("There was an error while saving file.", ex);
        }
    }
}