 using Microsoft.AspNetCore.Mvc;
 using ExcelDataReader;
 using Employee_App.Models;

 namespace Employee_App.FileProcessor;
 public class UnsupportedFileProcessor: IFileProcessor 
 {
     public bool ProcessFile() {
        return false;
     }

    public IEnumerable<IEmployee> Data {
        get => new List<IEmployee>();
    }
    
     public string Error {
         get => "The file that you provided is not supported.";
     }
 }