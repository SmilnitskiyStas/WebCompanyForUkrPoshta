using Microsoft.AspNetCore.Mvc.Rendering;
using WebCompany.Models.Dto;
using WebCompany.Models;
using WebCompany.Repositiories.IRepository;
using AutoMapper;
using WebCompany.Repositories.IRepository;

namespace WebCompany.Repositories
{
    public class FilterRepository : IFilterRepository
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IPhoneNmbRepository _phoneNmbRepository;
        private readonly IMapper _mapper;

        public FilterRepository(ICompanyRepository companyRepository, IDepartmentRepository departmentRepository,
            ICountryRepository countryRepository, ICityRepository cityRepository, IEmployeeRepository employeeRepository,
            IJobRepository jobRepository, IPhoneNmbRepository phoneNmbRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _departmentRepository = departmentRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _employeeRepository = employeeRepository;
            _jobRepository = jobRepository;
            _phoneNmbRepository = phoneNmbRepository;
            _mapper = mapper;
        }

        public List<SelectListItem> CreateCompanyList()
        {
            var companies = _mapper.Map<List<CompanyDto>>(_companyRepository.GetCompanies());

            List<SelectListItem> companyList = new List<SelectListItem>();

            foreach (var item in companies)
            {
                companyList.Add(new SelectListItem() { Text = item.CompanyName, Value = item.CompanyName });
            }

            return companyList;
        }

        public List<SelectListItem> CreateDepartmentList()
        {
            var departments = _mapper.Map<List<DepartmentDto>>(_departmentRepository.GetDepartments());

            List<SelectListItem> departmentList = new List<SelectListItem>();

            foreach (var department in departments)
            {
                departmentList.Add(new SelectListItem() { Text = department.DepartmentName, Value = department.DepartmentName });
            }

            return departmentList;
        }

        public List<SelectListItem> CreateCountryList()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var country in countries)
            {
                list.Add(new SelectListItem() { Text = country.CountryName, Value = country.CountryName });
            }

            return list;
        }

        public List<SelectListItem> CreateCityList()
        {
            var cities = _mapper.Map<List<CityDto>>(_cityRepository.GetCities());

            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var city in cities)
            {
                list.Add(new SelectListItem() { Text = city.CityName, Value = city.CityName });
            }

            return list;
        }

        public List<SelectListItem> CreateJobList()
        {
            var jogs = _mapper.Map<List<JobDto>>(_jobRepository.GetJobs());

            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var job in jogs)
            {
                list.Add(new SelectListItem() { Text = job.JobName, Value = job.JobName });
            }

            return list;
        }

        public List<SelectListItem> CreatePhoneNumberList()
        {
            var phoneNumbers = _mapper.Map<List<PhoneNmbDto>>(_phoneNmbRepository.GetPhones());

            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var phoneNmb in phoneNumbers)
            {
                list.Add(new SelectListItem() { Text = phoneNmb.PhoneNumber, Value = phoneNmb.PhoneNumber });
            }

            return list;
        }

        public List<SelectListItem> CreateEmployeeList()
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetEmployees());

            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var employee in employees)
            {
                list.Add(new SelectListItem()
                {
                    Text = $"{employee.SurName} {employee.FirstName} {employee.LastName}",
                    Value = $"{employee.SurName} {employee.FirstName} {employee.LastName}"
                });
            }

            return list;
        }
    }
}
