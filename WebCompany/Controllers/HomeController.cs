using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using WebCompany.Models;
using WebCompany.Models.Dto;
using WebCompany.Repositiories.IRepository;
using WebCompany.Repositories.IRepository;

namespace WebCompany.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFilterEmployeeRepository _filterEmployeeRepository;
        private readonly IFilterSalaryRepository _filterSalaryRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IPhoneNmbRepository _phoneNmbRepository;
        private readonly IMapper _mapper;

        public HomeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, ICompanyRepository companyRepository,
            ICountryRepository countryRepository, ICityRepository cityRepository, IJobRepository jobRepository,
            IPhoneNmbRepository phoneNmbRepository, IMapper mapper, IFilterEmployeeRepository filterEmployeeRepository, IFilterSalaryRepository filterSalaryRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _companyRepository = companyRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _jobRepository = jobRepository;
            _phoneNmbRepository = phoneNmbRepository;
            _mapper = mapper;
            _filterEmployeeRepository = filterEmployeeRepository;
            _filterSalaryRepository = filterSalaryRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetEmployees());
            ModelForEmployeeDto modelsForEmployee = new ModelForEmployeeDto();

            modelsForEmployee.Employees = employees;
            modelsForEmployee.FilterRequest = new FilterRequestDto();

            ViewBag.CompanyList = CreateCompanyList();
            ViewBag.DepartmentList = CreateDepartmentList();
            ViewBag.CountryList = CreateCountryList();
            ViewBag.CityList = CreateCityList();
            ViewBag.JobList = CreateJobList();
            ViewBag.PhoneNmbList = CreatePhoneNumberList();
            ViewBag.EmployeeList = CreateEmployeeList();

            return View(modelsForEmployee);
        }

        [HttpPost]
        public IActionResult Index(FilterRequestDto filterRequest)
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_filterEmployeeRepository.GetEmployeesOfFilters(CreateCommandForFilterSqlQuery(filterRequest)));

            ModelForEmployeeDto modelsForEmployee = new ModelForEmployeeDto();

            modelsForEmployee.Employees = employees;
            modelsForEmployee.FilterRequest = new FilterRequestDto()
            {
                CompanyName = filterRequest.CompanyName,
                DepartmentName = filterRequest.DepartmentName,
                CountryName = filterRequest.CountryName,
                CityName = filterRequest.CityName,
                JobName = filterRequest.JobName,
                PhoneNumber = filterRequest.PhoneNumber,
                EmployeeName = filterRequest.EmployeeName
            };

            ViewBag.CompanyList = CreateCompanyList();
            ViewBag.DepartmentList = CreateDepartmentList();
            ViewBag.CountryList = CreateCountryList();
            ViewBag.CityList = CreateCityList();
            ViewBag.JobList = CreateJobList();
            ViewBag.PhoneNmbList = CreatePhoneNumberList();
            ViewBag.EmployeeList = CreateEmployeeList();

            return View(modelsForEmployee);
        }

        [HttpGet("employee-info/{employeeId:int}")]
        public IActionResult UserInfo(int employeeId)
        {
            var employee = _mapper.Map<EmployeeDto>(_employeeRepository.GetEmployee(employeeId));

            return View(employee);
        }

        [HttpPost]
        public IActionResult UserEdit(EmployeeDto employee)
        {
            var emploeeForReplace = _employeeRepository.GetEmployee(employee.EmployeeId);

            if (ModelState.IsValid)
            {
                if (_employeeRepository.GetEmployee(employee.EmployeeId) == null)
                {
                    return NotFound();
                }

                emploeeForReplace.SurName = employee.SurName;
                emploeeForReplace.FirstName = employee.FirstName;
                emploeeForReplace.LastName = employee.LastName;
                emploeeForReplace.BirthDate = employee.BirthDate;
                emploeeForReplace.StartWorkDate = employee.StartWorkDate;
                emploeeForReplace.Salary_in_UAH = employee.Salary_in_UAH;

                _employeeRepository.UpdateEmployee(emploeeForReplace);
            }
            return Redirect("Index");
        }

        [HttpDelete("employee-delete/{employeeId:int}")]
        public IActionResult Delete(int employeeId)
        {
            _employeeRepository.DeleteEmployee(employeeId);

            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult SalaryReport()
        {
            ModelForSalaryDto modelForSalary = new ModelForSalaryDto();
            var amountSalaries = _filterSalaryRepository.GetTotalSalary("");

            modelForSalary.Salaries = amountSalaries;
            modelForSalary.FilterRequest = new FilterRequestDto();

            ViewBag.CompanyList = CreateCompanyList();
            ViewBag.DepartmentList = CreateDepartmentList();
            ViewBag.CountryList = CreateCountryList();
            ViewBag.CityList = CreateCityList();
            ViewBag.JobList = CreateJobList();
            ViewBag.PhoneNmbList = CreatePhoneNumberList();
            ViewBag.EmployeeList = CreateEmployeeList();

            return View(modelForSalary);
        }

        [HttpPost]
        public IActionResult SalaryReport(FilterRequestDto filterRequest)
        {
            ModelForSalaryDto modelForSalary = new ModelForSalaryDto();

            modelForSalary.Salaries = _filterSalaryRepository.GetTotalSalary(CreateCommandForFilterSqlQuery(filterRequest));
            modelForSalary.FilterRequest = new FilterRequestDto()
            {
                CompanyName = filterRequest.CompanyName,
                DepartmentName = filterRequest.DepartmentName,
                CountryName = filterRequest.CountryName,
                CityName = filterRequest.CityName,
                JobName = filterRequest.JobName
            };

            ViewBag.CompanyList = CreateCompanyList();
            ViewBag.DepartmentList = CreateDepartmentList();
            ViewBag.CountryList = CreateCountryList();
            ViewBag.CityList = CreateCityList();
            ViewBag.JobList = CreateJobList();
            ViewBag.PhoneNmbList = CreatePhoneNumberList();
            ViewBag.EmployeeList = CreateEmployeeList();

            return View(modelForSalary);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string CreateCommandForFilterSqlQuery(FilterRequestDto filterRequest)
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

        private List<SelectListItem> CreateCompanyList()
        {
            var companies = _mapper.Map<List<CompanyDto>>(_companyRepository.GetCompanies());

            List<SelectListItem> companyList = new List<SelectListItem>();

            foreach (var item in companies)
            {
                companyList.Add(new SelectListItem() { Text = item.CompanyName, Value = item.CompanyName });
            }

            return companyList;
        }

        private List<SelectListItem> CreateDepartmentList()
        {
            var departments = _mapper.Map<List<DepartmentDto>>(_departmentRepository.GetDepartments());

            List<SelectListItem> departmentList = new List<SelectListItem>();

            foreach (var department in departments)
            {
                departmentList.Add(new SelectListItem() { Text = department.DepartmentName, Value = department.DepartmentName });
            }

            return departmentList;
        }

        private List<SelectListItem> CreateCountryList()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var country in countries)
            {
                list.Add(new SelectListItem() { Text = country.CountryName, Value = country.CountryName });
            }

            return list;
        }

        private List<SelectListItem> CreateCityList()
        {
            var cities = _mapper.Map<List<CityDto>>(_cityRepository.GetCities());

            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var city in cities)
            {
                list.Add(new SelectListItem() { Text = city.CityName, Value = city.CityName });
            }

            return list;
        }

        private List<SelectListItem> CreateJobList()
        {
            var jogs = _mapper.Map<List<JobDto>>(_jobRepository.GetJobs());

            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var job in jogs)
            {
                list.Add(new SelectListItem() { Text = job.JobName, Value = job.JobName });
            }

            return list;
        }

        private List<SelectListItem> CreatePhoneNumberList()
        {
            var phoneNumbers = _mapper.Map<List<PhoneNmbDto>>(_phoneNmbRepository.GetPhones());

            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var phoneNmb in phoneNumbers)
            {
                list.Add(new SelectListItem() { Text = phoneNmb.PhoneNumber, Value = phoneNmb.PhoneNumber });
            }

            return list;
        }

        private List<SelectListItem> CreateEmployeeList()
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
