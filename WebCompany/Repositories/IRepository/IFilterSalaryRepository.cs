using WebCompany.Models.Dto;

namespace WebCompany.Repositories.IRepository
{
    public interface IFilterSalaryRepository
    {
        ICollection<FilterSalaryDto> GetTotalSalary(string filters);
    }
}
