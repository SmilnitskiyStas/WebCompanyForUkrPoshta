using WebCompany.Models;

namespace WebCompany.Repositiories.IRepository
{
    public interface IJobRepository
    {
        ICollection<Job> GetJobs();
        Job GetJob(int id);
        Job GetJob(string jobName);
        Job CreateJob(Job job);
        Job UpdateJob(Job job);
        bool DeleteJob(int id);
    }
}
