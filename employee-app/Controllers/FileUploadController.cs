using System.Net;
using Microsoft.AspNetCore.Mvc;
using Employee_App.FileProcessor;
using Employee_App.FileUpload;

namespace Employee_App.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FileUploadController : ControllerBase
{
	private readonly IFileProcessorFactory _fileProcesFactory;
	private readonly IFileUploadService  _fileUploadService;
	public FileUploadController(IFileUploadService fileUploadService, IFileProcessorFactory fileProcesFactory)
    {
        _fileProcesFactory = fileProcesFactory;
		_fileUploadService = fileUploadService;
    }

	public async Task<ActionResult> Post(IFormFile file)
	{
		if (file != null && file.Length > 0)
        {			
			try
			{
				if (await _fileUploadService.UploadFile(file))
				{
					IFileProcessor fileProcessor = _fileProcesFactory.CreateFileProcessor(_fileUploadService.FilePath);
					if (fileProcessor != null) {
						if (fileProcessor.ProcessFile()) {
							return Ok(fileProcessor.Data);
						} else {
							return StatusCode((int)HttpStatusCode.NotImplemented, fileProcessor.Error);	
						}
					} else {
						return StatusCode((int)HttpStatusCode.NotImplemented, "The file that you uploaded is not supported.");
					}
				}
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
        }

		return StatusCode((int)HttpStatusCode.InternalServerError, "There was an error while processing your request.");
	}
}

