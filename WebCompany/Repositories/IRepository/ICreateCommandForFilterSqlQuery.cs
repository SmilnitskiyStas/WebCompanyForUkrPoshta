using WebCompany.Models.Dto;

namespace WebCompany.Repositories.IRepository
{
    public interface ICreateCommandForFilterSqlQuery
    {
        string CreateCommandForSql(FilterRequestDto filterRequest);
    }
}
