namespace Employee_App.Models;

public interface IEmployee
{
	string FirstName { get; set; }
	string LastName { get; set; }
	string Number { get; set; }
	string Status { get; set; }
}