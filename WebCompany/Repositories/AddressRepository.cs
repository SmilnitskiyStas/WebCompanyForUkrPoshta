using WebCompany.Models;
using WebCompany.Repositiories.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WebCompany.Repositiories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string _connectionString;

        public AddressRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Address CreateAddress(Address address)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Addresses (Street, CountryId, CityId)" +
                    $"VALUES ('{address.Street}', '{address.CountryId}', '{address.CityId}'); SELECT CAST (SCOPE_IDENTITY() AS int);";

                return db.Query<Address>(sqlQuery).FirstOrDefault();
            }
        }

        public bool DeleteAddress(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"DELETE FROM Addresses WHERE AddressId = {id}; SELECT CAST (SCOPE_IDENTITY() AS int);";

                return db.Query<object>(sqlQuery) is null ? true : false;
            }
        }

        public Address GetAddress(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Address>($"SELECT * FROM Address WHERE AddressId = {id}").FirstOrDefault();
            }
        }

        public Address GetAddress(string name)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Address>($"SELECT * FROM Address WHERE Street = '{name}'").FirstOrDefault();
            }
        }

        public ICollection<Address> GetAddresses()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Address>($"SELECT * FROM Address").ToList();
            }
        }

        public Address UpdateAddress(Address address)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute($"UPDATE Addresses SET Street = '{address.Street}', " +
                    $"CountryId = '{address.CountryId}', CityId = '{address.CityId}' WHERE AddressId = {address.AddressId};");

                return GetAddress(address.AddressId);
            }
        }
    }
}
