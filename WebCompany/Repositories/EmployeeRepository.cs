using WebCompany.Models;
using WebCompany.Repositiories.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WebCompany.Repositiories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string connectionString;

        public EmployeeRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Employee CreateEmployee(Employee employee)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"INSERT INTO Employees (SurName, FirstName, LastName, BirthDate, StartWordDate, Salary_in_UAH, CompanyId," +
                    $" EmployeePhoneId, EmployeeAddressId, EmployeeJobId) VALUES ('{employee.SurName}', '{employee.FirstName}', '{employee.LastName}'," +
                    $" '{employee.BirthDate}', '{employee.StartWorkDate}', '{employee.Salary_in_UAH}', '{employee.CompanyId}', '{employee.EmployeePhoneId}'," +
                    $" '{employee.EmpoyeeAddressId}', '{employee.EmployeeJobId}');" +
                    $" SELECT CAST (SCOPE_IDENTITY() AS int);";

                return db.Query<Employee>(sqlQuery).FirstOrDefault();
            }
        }

        public bool DeleteEmployee(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"DELETE FROM Employees WHERE EmployeeId = {id}; SELECT CAST (SCOPE_IDENTITY() AS int);";

                return db.Query<object>(sqlQuery) is null ? true : false;
            }
        }

        public Employee GetEmployee(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Employee>($"SELECT * FROM Employees WHERE EmployeeId = {id}").FirstOrDefault();
            }
        }

        public Employee GetEmployee(string employeeName)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Employee>($"SELECT * FROM Employees WHERE EmployeeName = '{employeeName}'").FirstOrDefault();
            }
        }

        public ICollection<Employee> GetEmployees()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Employee>($"SELECT * FROM Employees").ToList();
            }
        }

        public Employee UpdateEmployee(Employee employee)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute($"UPDATE Employees SET SurName = '{employee.SurName}', FirstName = '{employee.FirstName}', LastName = '{employee.LastName}', " +
                    $"BirthDate = '{employee.BirthDate}', StartWordDate = '{employee.StartWorkDate}', Salary_in_UAH = '{employee.Salary_in_UAH}'," +
                    $" EmployeePhoneId = '{employee.EmployeePhoneId}', EmployeeAddressId = '{employee.EmpoyeeAddressId}', EmployeeJobId = '{employee.EmployeeJobId}'" +
                    $" WHERE EmployeeId = {employee.EmployeeId};");

                return GetEmployee(employee.EmployeeId);
            }
        }
    }
}
