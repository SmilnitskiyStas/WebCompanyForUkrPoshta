using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebCompany.Repositories.IRepository
{
    public interface IFilterRepository
    {
        List<SelectListItem> CreateCompanyList();
        List<SelectListItem> CreateDepartmentList();
        List<SelectListItem> CreateCountryList();
        List<SelectListItem> CreateCityList();
        List<SelectListItem> CreateJobList();
        List<SelectListItem> CreatePhoneNumberList();
        List<SelectListItem> CreateEmployeeList();
    }
}
