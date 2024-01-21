using WebCompany.Models;
using WebCompany.Models.Dto;

namespace WebCompany.Repositories.IRepository
{
    public interface IFilterEmployeeRepository
    {
        ICollection<Employee> GetEmployees(FilterRequestDto filter);
    }
}
