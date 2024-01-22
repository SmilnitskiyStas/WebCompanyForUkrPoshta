using WebCompany.Models;
using WebCompany.Repositiories.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebCompany.Models.Dto;

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
                    $" '{employee.EmployeeAddressId}', '{employee.EmployeeJobId}');" +
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

        public ICollection<Employee> GetEmployeesOfFilters(string filters)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"SELECT " +
                $"e.SurName" +
                $", e.FirstName" +
                $", e.LastName" +
                $", e.BirthDate" +
                $", e.StartWorkDate" +
                $", e.Salary_in_UAH " +
                $"FROM Employees AS e " +
                $"LEFT JOIN PhoneNumbers AS pn ON e.EmployeePhoneId = pn.PhoneId " +
                $"LEFT JOIN Jobs AS j ON e.EmployeeJobId = j.Jobid " +
                $"LEFT JOIN Departments AS d ON j.DepartmentId = d.DepartmentId " +
                $"LEFT JOIN Companies AS cmp ON e.CompanyId = cmp.CompanyId " +
                $"LEFT JOIN Addresses AS a ON e.EmployeeAddressId = a.AddressId " +
                $"LEFT JOIN Cities AS cty ON a.CityId = cty.CityId " +
                $"LEFT JOIN Countries AS ct ON a.CountryId = ct.CountryId " +
                $"WHERE 1=1 " +
                $"{filters}";

                return db.Query<Employee>(sqlQuery).ToList();
            }
        }

        public Employee UpdateEmployee(Employee employee)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute($"UPDATE Employees SET SurName = '{employee.SurName}', FirstName = '{employee.FirstName}', LastName = '{employee.LastName}', " +
                    $"BirthDate = '{employee.BirthDate.Year}-{employee.BirthDate.Month}-{employee.BirthDate.Day}'," +
                    $" StartWorkDate = '{employee.StartWorkDate.Year}-{employee.StartWorkDate.Month}-{employee.StartWorkDate.Day}'," +
                    $" Salary_in_UAH = '{employee.Salary_in_UAH}'," +
                    $" EmployeePhoneId = '{employee.EmployeePhoneId}', EmployeeAddressId = '{employee.EmployeeAddressId}', EmployeeJobId = '{employee.EmployeeJobId}'" +
                    $" WHERE EmployeeId = {employee.EmployeeId};");

                return GetEmployee(employee.EmployeeId);
            }
        }
    }
}
