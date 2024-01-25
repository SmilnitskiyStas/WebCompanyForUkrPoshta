using WebCompany.Models;
using WebCompany.Repositiories.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WebCompany.Repositiories
{
    public class PhoneNmbRepository : IPhoneNmbRepository
    {
        private readonly string _connectionString;

        public PhoneNmbRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public PhoneNmb CreatePhoneNmb(PhoneNmb phoneNmb)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO PhoneNumbers (PhoneNumber) VALUES ('{phoneNmb.PhoneNumber}'); SELECT CAST (SCOPE_IDENTITY() AS int);";
                return db.Query<PhoneNmb>(sqlQuery).FirstOrDefault();
            }
        }

        public bool DeletePhoneNmb(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"DELETE FROM PhoneNumbers WHERE PhoneId = {id}; SELECT CAST (SCOPE_IDENTITY() AS int);";

                return db.Query<object>(sqlQuery) is null ? true : false;
            }
        }

        public PhoneNmb GetPhone(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<PhoneNmb>($"SELECT * FROM PhoneNumbers WHERE PhoneId = {id}").FirstOrDefault();
            }
        }

        public PhoneNmb GetPhone(string phoneNumber)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<PhoneNmb>($"SELECT * FROM PhoneNumbers WHERE PhoneNumber = '{phoneNumber}'").FirstOrDefault();
            }
        }

        public ICollection<PhoneNmb> GetPhones()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<PhoneNmb>($"SELECT * FROM PhoneNumbers").ToList();
            }
        }

        public PhoneNmb UpdatePhoneNmb(PhoneNmb phoneNmb)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                db.Execute($"UPDATE PhoneNumbers SET PhoneNumber = '{phoneNmb.PhoneNumber}' WHERE PhoneId = {phoneNmb.PhoneId};");

                return GetPhone(phoneNmb.PhoneId);
            }
        }
    }
}
