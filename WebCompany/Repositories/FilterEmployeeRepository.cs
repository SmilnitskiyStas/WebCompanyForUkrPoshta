using WebCompany.Models;
using WebCompany.Models.Dto;
using WebCompany.Repositiories.IRepository;
using WebCompany.Repositories.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WebCompany.Repositories
{
    public class FilterEmployeeRepository : IFilterEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IPhoneNmbRepository _phoneNmbRepository;

        public FilterEmployeeRepository(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository,
            ICountryRepository countryRepository, ICityRepository cityRepository,
            IJobRepository jobRepository, IPhoneNmbRepository phoneNmbRepository, IConfiguration configuration)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _jobRepository = jobRepository;
            _phoneNmbRepository = phoneNmbRepository;
            _configuration = configuration;
        }

        public ICollection<Employee> GetEmployees(FilterRequestDto filter)
        {
            var department = _departmentRepository.GetDepartment(filter.DepartmentName);
            var country = _countryRepository.GetCountry(filter.CountryName);
            var city = _cityRepository.GetCity(filter.CityName);
            var job = _jobRepository.GetJob(filter.JobName);
            var phoneNmb = _phoneNmbRepository.GetPhone(filter.PhoneNumber);

            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sqlQuery = "";
            }

            return null;
        }
    }
}
