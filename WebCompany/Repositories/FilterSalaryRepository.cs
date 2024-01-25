using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebCompany.Models.Dto;
using WebCompany.Repositories.IRepository;

namespace WebCompany.Repositories
{
    public class FilterSalaryRepository : IFilterSalaryRepository
    {
        private readonly string _connectionString;

        public FilterSalaryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ICollection<FilterSalaryDto> GetTotalSalary(string filters)
        {
            using (IDbConnection db =  new SqlConnection(_connectionString)) 
            {
                var sqlQuery = $"SELECT " +
                        $"d.DepartmentId AS FilterSalaryId" +
                        $", d.DepartmentName AS FilterSalaryName" +
                        $", sum(e.Salary_in_UAH) AS TotalAmountSalary " +
                    $"FROM Employees AS e " +
                    $"LEFT JOIN Jobs AS j ON e.EmployeeJobId = j.Jobid " +
                    $"LEFT JOIN Departments AS d ON j.DepartmentId = d.DepartmentId " +
                    $"LEFT JOIN Companies AS cmp ON e.CompanyId = cmp.CompanyId " +
                    $"LEFT JOIN Addresses AS a ON e.EmployeeAddressId = a.AddressId " +
                    $"LEFT JOIN Cities AS cty ON a.CityId = cty.CityId " +
                    $"LEFT JOIN Countries AS ct ON a.CountryId = ct.CountryId " +
                    $"WHERE 1=1 " +
                    $"{filters} " +
                    $"GROUP BY d.DepartmentId, d.DepartmentName;";

                return db.Query<FilterSalaryDto>(sqlQuery).ToList();
            }
        }
    }
}
