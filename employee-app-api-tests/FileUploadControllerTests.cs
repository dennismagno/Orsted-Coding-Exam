using Moq;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Employee_App.Models;
using Employee_App.FileProcessor;
using Employee_App.FileUpload;
using Employee_App.Controllers;

namespace FileUploadControllerTests;

public class FileProcessorTests
{
    byte[] bytes = Encoding.UTF8.GetBytes("This is a dummy data");

    [Fact]
    public async Task File_Upload_File_Controller_501_Response()
    {
        // Arrange
        var fileUploadMock = new Mock<IFileUploadService>();
        fileUploadMock.Setup(_ => _.UploadFile(It.IsAny<IFormFile>())).ReturnsAsync(true);

        var fileProcessorMock = new Mock<IFileProcessor>();
        fileProcessorMock.Setup(_ => _.ProcessFile()).Returns(false);
        fileProcessorMock.Setup(_ => _.Error).Returns("The file provided is not on a correct format.");

        var fileProcessorFactoryMock = new Mock<IFileProcessorFactory>();
        fileProcessorFactoryMock.Setup(_ => _.CreateFileProcessor(It.IsAny<string>())).Returns(fileProcessorMock.Object);

        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.xlsx");

        // Act
        var fileUploadController = new FileUploadController(fileUploadMock.Object, fileProcessorFactoryMock.Object);
        var result = await fileUploadController.Post(file);

        //Assert
        ObjectResult objectResponse = Assert.IsType<ObjectResult>(result); 
        Assert.True((int)HttpStatusCode.NotImplemented == objectResponse.StatusCode,"The http response code should be 501."); 
    }

     [Fact]
    public async Task File_Upload_File_Controller_Return_Data()
    {
        // Arrange
        List<IEmployee> employeeData = new List<IEmployee>();
        employeeData.Add(new Employee { Number = "001", FirstName = "Dennis", LastName = "Magno", Status = "Regular" });

        var fileUploadMock = new Mock<IFileUploadService>();
        fileUploadMock.Setup(_ => _.UploadFile(It.IsAny<IFormFile>())).ReturnsAsync(true);

        var fileProcessorMock = new Mock<IFileProcessor>();
        fileProcessorMock.Setup(_ => _.ProcessFile()).Returns(true);
        fileProcessorMock.Setup(_ => _.Data).Returns(employeeData);

        var fileProcessorFactoryMock = new Mock<IFileProcessorFactory>();
        fileProcessorFactoryMock.Setup(_ => _.CreateFileProcessor(It.IsAny<string>())).Returns(fileProcessorMock.Object);

        IFormFile file = new FormFile(new MemoryStream(bytes), 0, bytes.Length, "Data", "dummy.xlsx");
        
        // Act
        var fileUploadController = new FileUploadController(fileUploadMock.Object, fileProcessorFactoryMock.Object);
        var result = await fileUploadController.Post(file);
        OkObjectResult objectResponse = Assert.IsType<OkObjectResult>(result); 
        var valueData = objectResponse.Value as IEnumerable<IEmployee>;

        //Assert
        Assert.True((int)HttpStatusCode.OK == objectResponse.StatusCode,"The http response code should be 200.");
        Assert.NotEmpty(valueData); 
        Assert.Same(employeeData, valueData);
    }
}