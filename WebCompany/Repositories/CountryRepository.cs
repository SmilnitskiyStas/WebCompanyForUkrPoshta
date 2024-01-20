using WebCompany.Models;
using WebCompany.Repositiories.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace WebCompany.Repositiories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly string connectionString;

        public CountryRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Country CreateCountry(Country country)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"INSERT INTO Countries (CountryName) VALUES ('{country.CountryName}'); SELECT CAST (SCOPE_IDENTITY() AS int);";

                return db.Query<Country>(sqlQuery).FirstOrDefault();
            }
        }

        public bool DeleteCountry(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"DELETE FROM Countries WHERE CountryId = {id}; SELECT CAST (SCOPE_IDENTITY() AS int);";

                return db.Query<object>(sqlQuery) is null ? true : false;
            }
        }

        public ICollection<Country> GetCountries()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Country>($"SELECT * FROM Countries").ToList();
            }
        }

        public Country GetCountry(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Country>($"SELECT * FROM Countries WHERE CountryId = {id}").FirstOrDefault();
            }
        }

        public Country GetCountry(string countryName)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Country>($"SELECT * FROM Countries WHERE CountryName = '{countryName}'").FirstOrDefault();
            }
        }

        public Country UpdateCountry(Country country)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute($"UPDATE Countries SET CountryName = '{country.CountryName}' WHERE CountryId = {country.CountryId};");

                return GetCountry(country.CountryId);
            }
        }
    }
}
