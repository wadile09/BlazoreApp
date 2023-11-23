namespace BlazoreApp.Model
{
	public class Employee
	{
		public int Id { get; set; }	
		public string EmployeeFName { get; set; }	
		public string EmployeeLName { get; set; }	
		public string EmployeeDepartment { get; set; }	
		public int DepartmentId { get; set; }	
		public decimal Salary { get; set; }
		public decimal BirthDate { get; set; }
	}
}
