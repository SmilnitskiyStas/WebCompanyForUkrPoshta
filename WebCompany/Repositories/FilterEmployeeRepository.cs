using WebCompany.Models;
using WebCompany.Models.Dto;
using WebCompany.Repositiories.IRepository;
using WebCompany.Repositories.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WebCompany.Repositories
{
    public class FilterEmployeeRepository : IFilterEmployeeRepository
    {
        private readonly string _connectionString;

        public FilterEmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ICollection<Employee> GetEmployeesOfFilters(string filters)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"SELECT " +
                $"e.EmployeeId" +
                $", e.SurName" +
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

                var emp = db.Query<Employee>(sqlQuery).ToList();

                return emp;
            }
        }
    }
}
