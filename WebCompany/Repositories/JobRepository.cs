using WebCompany.Models;
using WebCompany.Repositiories.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

namespace WebCompany.Repositiories
{
    public class JobRepository : IJobRepository
    {
        private readonly string connectionString;

        public JobRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Job CreateJob(Job job)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"INSERT INTO Jobs (JobName, DepartmentId) VALUES ('{job.JobName}', '{job.DepartmentId}'); SELECT CAST (SCOPE_IDENTITY() AS int);";
                return db.Query<Job>(sqlQuery).FirstOrDefault();
            }
        }
        public bool DeleteJob(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"DELETE FROM Jobs WHERE JobId = {id}; SELECT CAST (SCOPE_IDENTITY() AS int);";

                return db.Query<object>(sqlQuery) is null ? true : false;
            }
        }

        public Job GetJob(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Job>($"SELECT * FROM Jobs WHERE JobId = {id}").FirstOrDefault();
            }
        }

        public Job GetJob(string jobName)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Job>($"SELECT * FROM Jobs WHERE JobName = '{jobName}'").FirstOrDefault();
            }
        }

        public ICollection<Job> GetJobs()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Job>($"SELECT * FROM Jobs").ToList();
            }
        }

        public Job UpdateJob(Job job)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute($"UPDATE Addresses SET JobName = '{job.JobName}', DepartmentId= '{job.DepartmentId}' WHERE JobId = {job.JobId};");

                return GetJob(job.JobId);
            }
        }
    }
}
