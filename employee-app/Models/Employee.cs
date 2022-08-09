namespace Employee_App.Models;

public class Employee : IEmployee
{
	public string Number { get; set; }

	public string FirstName { get; set; }

	public string LastName { get; set; }

	public string Status { get; set; }
}
