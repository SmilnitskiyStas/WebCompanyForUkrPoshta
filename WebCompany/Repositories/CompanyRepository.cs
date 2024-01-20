using WebCompany.Models;
using WebCompany.Repositiories.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace WebCompany.Repositiories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly string connectionString;

        public CompanyRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Company CreateCompany(Company company)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"INSERT INTO Companies (CompanyName, CompanyNumberId, CompanyAddressId) VALUES ('{company.CompanyName}'," +
                    $" '{company.CompanyNumberId}', '{company.CompanyAddressId}'); SELECT CAST (SCOPE_IDENTITY() AS int);";

                return db.Query<Company>(sqlQuery).FirstOrDefault();
            }
        }

        public bool DeleteCompany(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"DELETE FROM Companies WHERE CompanyId = {id}; SELECT CAST (SCOPE_IDENTITY() AS int);";

                return db.Query<object>(sqlQuery) is null ? true : false;
            }
        }

        public ICollection<Company> GetCompanies()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Company>($"SELECT * FROM Companies").ToList();
            }
        }

        public Company GetCompany(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Company>($"SELECT * FROM Companies WHERE CompanyId = {id}").FirstOrDefault();
            }
        }

        public Company GetCompany(string name)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Company>($"SELECT * FROM Companies WHERE CompanyName = '{name}'").FirstOrDefault();
            }
        }

        public Company UpdateCompany(Company company)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute($"UPDATE Companies SET CompanyName = '{company.CompanyName}', CompanyNumberId = '{company.CompanyNumberId}'," +
                    $" CompanyAddressId = '{company.CompanyAddressId}' WHERE CompanyId = {company.CompanyId};");

                return GetCompany(company.CompanyId);
            }
        }
    }
}
