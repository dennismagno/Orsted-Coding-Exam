namespace Employee_App.FileUpload;
public interface IFileUploadService
{
    string FilePath { get; }
    Task<bool> UploadFile(IFormFile file);
}