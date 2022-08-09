 using Microsoft.AspNetCore.Mvc;
 using ExcelDataReader;
 using Employee_App.Models;

 namespace Employee_App.FileProcessor;

 public class ExcelFileProcessor: IFileProcessor 
 {
    string _filePath = string.Empty;
    List<IEmployee> _employee = new List<IEmployee>();
    string _error = string.Empty;

    public ExcelFileProcessor(string filePath) {
        _filePath = filePath;
    }

    public bool ProcessFile() {
        if (System.IO.File.Exists(_filePath) && Path.GetExtension(_filePath).ToLower() != ".xlsx") {
            _error = "There was an error processing the file provided.";
            return false;
        }

        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        using (var stream = File.Open(_filePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = ExcelReaderFactory.CreateReader(stream)) 
            {
                while (reader.Read()) 
                {
                    if (reader.Depth == 0) {
                        if (!ValidateHeaders(reader)) {
                            _error = "The file provided is not on a correct format.";
                            break;
                        }
                    } else {
                        _employee.Add(
                            new Employee { Number = reader.GetValue(0).ToString(), 
                                            FirstName = reader.GetValue(1).ToString(),
                                            LastName = reader.GetValue(2).ToString(),
                                            Status= reader.GetValue(3).ToString() });
                    }
                }
            }
        }
        
        if (_error != "") return false;
        
        return true;
    }

    private bool ValidateHeaders(IExcelDataReader reader) {
        return reader.GetValue(0).ToString() == "Employee Number" &&
               reader.GetValue(1).ToString() == "First Name" &&
               reader.GetValue(2).ToString() == "Last Name" &&
               reader.GetValue(3).ToString() == "Employee Status";
    }

    public IEnumerable<IEmployee> Data {
        get => _employee.AsEnumerable();
    }

    public string Error {
        get => _error;
    }
 }