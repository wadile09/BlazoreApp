using BlazoreApp.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace BlazoreApp.Data
{
	public class EmployeeService
	{

		public IConfiguration Configuration { get; }
		string connectionString;
		public EmployeeService(IConfiguration configuration)
		{
			Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			connectionString = Configuration.GetConnectionString("myDbConnection");

		}
		public List<Employee> GetData()
		{
			List<Employee> employeesList = new List<Employee>();

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand("GetEmployeeData", sqlConnection))
				{
					command.CommandType = CommandType.StoredProcedure;
					sqlConnection.Open();
					using (SqlDataReader sdr = command.ExecuteReader())
					{
						while (sdr.Read())
						{
							var employee = new Employee()
							{
								Id = (int)sdr["Id"],
								EmployeeFName = (string)sdr["FirstName"],
								EmployeeLName = (string)sdr["LastName"],
								EmployeeDepartment = (string)sdr["DepartmentName"],
								DepartmentId = (int)sdr["DepId"],
								Salary = (decimal)sdr["Salary"],
							};
							employeesList.Add(employee);
						}
					}
					return employeesList;
				}
			}

		}
		public Employee GetEmployee(int id)
		{

			List<Employee> employeesList = new List<Employee>();
			employeesList = GetData();
			return employeesList.SingleOrDefault(x => x.Id == id);

		}
		public void AddOrUpdateEmployee(Employee employee)
		{

			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand("usp_AddOrUpdateEmployee", sqlConnection))
				{
					command.CommandType = CommandType.StoredProcedure;
					sqlConnection.Open();

					command.Parameters.AddWithValue("@Id", employee.Id);
					command.Parameters.AddWithValue("@FirstName", employee.EmployeeFName);
					command.Parameters.AddWithValue("@LastName", employee.EmployeeLName);
					command.Parameters.AddWithValue("@Depatment ", employee.EmployeeDepartment);
					command.Parameters.AddWithValue("@DepatmentId", employee.DepartmentId);
					command.Parameters.AddWithValue("@Salry", employee.Salary);
					command.ExecuteNonQuery();
				}
			}
		}


	}
}