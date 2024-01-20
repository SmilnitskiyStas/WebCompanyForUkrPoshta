using WebCompany.Models;
using WebCompany.Repositiories.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

namespace WebCompany.Repositiories
{
    public class CityRepository : ICityRepository
    {
        private readonly string connectionString;

        public CityRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public City CreateCity(City city)
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"INSERT INTO Cities (CityName) VALUES ('{city.CityName}'); SELECT CAST (SCOPE_IDENTITY() AS int);";
                return db.Query<City>(sqlQuery).FirstOrDefault();
            }
        }

        public bool DeleteCity(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"DELETE FROM Cities WHERE CityId = {id}; SELECT CAST (SCOPE_IDENTITY() AS int);";

                return db.Query<object>(sqlQuery) is null ? true : false;
            }
        }

        public ICollection<City> GetCities()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<City>($"SELECT * FROM Cities").ToList();
            }
        }

        public City GetCity(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<City>($"SELECT * FROM Cities WHERE CityId = {id}").FirstOrDefault();
            }
        }

        public City GetCity(string cityName)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<City>($"SELECT * FROM Cities WHERE CityName = '{cityName}'").FirstOrDefault();
            }
        }

        public City UpdateCity(City city)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute($"UPDATE Cities SET CityName = '{city.CityName}' WHERE CityId = {city.CityId};");

                return GetCity(city.CityId);
            }
        }
    }
}
