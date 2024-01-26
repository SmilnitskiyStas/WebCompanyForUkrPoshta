using WebCompany.Models;
using WebCompany.Repositiories.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.Metrics;

namespace WebCompany.Repositiories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Department CreateDepartment(Department department)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Departments (DepartmentName, CompanyId) VALUES ('{department.DepartmentName}', '{department.CompanyId}');" +
                    $" SELECT CAST (SCOPE_IDENTITY() AS int);";

                return db.Query<Department>(sqlQuery).FirstOrDefault();
            }
        }

        public bool DeleteDepartment(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"DELETE FROM Departments WHERE DepartmentId = {id}; SELECT CAST (SCOPE_IDENTITY() AS int);";

                return db.Query<object>(sqlQuery) is null ? true : false;
            }
        }

        public Department GetDepartment(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Department>($"SELECT * FROM Departments WHERE DepartmentId = {id}").FirstOrDefault();
            }
        }

        public Department GetDepartment(string name)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Department>($"SELECT * FROM Departments WHERE DepartmentName = '{name}'").FirstOrDefault();
            }
        }

        public ICollection<Department> GetDepartments()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Department>($"SELECT * FROM Departments").ToList();
            }
        }

        public Department UpdateDepartment(Department department)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute($"UPDATE Departments SET DepartmentName = '{department.DepartmentName}', CompanyId = '{department.CompanyId}' WHERE DepartmentId = {department.DepartmentId};");

                return GetDepartment(department.DepartmentId);
            }
        }
    }
}
