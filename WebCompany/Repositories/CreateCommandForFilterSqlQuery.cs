using WebCompany.Models.Dto;
using WebCompany.Repositories.IRepository;

namespace WebCompany.Repositories
{
    public class CreateCommandForFilterSqlQuery : ICreateCommandForFilterSqlQuery
    {
        public string CreateCommandForSql(FilterRequestDto filterRequest)
        {
            var sqlQueryFilter = "";

            if (filterRequest.CompanyName is not null)
            {
                sqlQueryFilter += $"AND cmp.CompanyName = '{filterRequest.CompanyName}' ";
            }

            if (filterRequest.DepartmentName is not null)
            {
                sqlQueryFilter += $"AND d.DepartmentName = '{filterRequest.DepartmentName}' ";
            }

            if (filterRequest.EmployeeName is not null)
            {
                sqlQueryFilter += $"AND (e.SurName + ' ' + e.FirstName + ' ' + e.LastName) = '{filterRequest.EmployeeName}' ";
            }

            if (filterRequest.CountryName is not null)
            {
                sqlQueryFilter += $"AND ct.CountryName = '{filterRequest.CountryName}' ";
            }

            if (filterRequest.CityName is not null)
            {
                sqlQueryFilter += $"AND cty.CityName = '{filterRequest.CityName}' ";
            }

            if (filterRequest.JobName is not null)
            {
                sqlQueryFilter += $"AND j.JobName = '{filterRequest.JobName}' ";
            }

            if (filterRequest.PhoneNumber is not null)
            {
                sqlQueryFilter += $"AND pn.PhoneNumber = '{filterRequest.PhoneNumber}' ";
            }

            return sqlQueryFilter;
        }
    }
}
