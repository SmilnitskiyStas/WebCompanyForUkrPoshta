using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IFilterRepository _filterRepository;
        private readonly ICreateCommandForFilterSqlQuery _createCommandForFilterSqlQuery;
        private readonly IMapper _mapper;

        public HomeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, ICompanyRepository companyRepository,
            ICountryRepository countryRepository, ICityRepository cityRepository, IJobRepository jobRepository, IPhoneNmbRepository phoneNmbRepository,
            IMapper mapper, IFilterEmployeeRepository filterEmployeeRepository, IFilterSalaryRepository filterSalaryRepository,
            IFilterRepository filterRepository, ICreateCommandForFilterSqlQuery createCommandForFilterSqlQuery)
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
            _filterRepository = filterRepository;
            _createCommandForFilterSqlQuery = createCommandForFilterSqlQuery;
        }

        [HttpGet]
        [Route("/")]
        [Route("/index")]
        public IActionResult Index()
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetEmployees());
            ModelForEmployeeDto modelsForEmployee = new ModelForEmployeeDto();

            modelsForEmployee.Employees = employees;
            modelsForEmployee.FilterRequest = new FilterRequestDto();

            ViewBag.CompanyList = _filterRepository.CreateCompanyList();
            ViewBag.DepartmentList = _filterRepository.CreateDepartmentList();
            ViewBag.CountryList = _filterRepository.CreateCountryList();
            ViewBag.CityList = _filterRepository.CreateCityList();
            ViewBag.JobList = _filterRepository.CreateJobList();
            ViewBag.PhoneNmbList = _filterRepository.CreatePhoneNumberList();
            ViewBag.EmployeeList = _filterRepository.CreateEmployeeList();

            return View(modelsForEmployee);
        }

        [HttpPost]
        [Route("/")]
        [Route("/index")]
        public IActionResult Index(FilterRequestDto filterRequest)
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_filterEmployeeRepository
                .GetEmployeesOfFilters(_createCommandForFilterSqlQuery.CreateCommandForSql(filterRequest)));

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

            ViewBag.CompanyList = _filterRepository.CreateCompanyList();
            ViewBag.DepartmentList = _filterRepository.CreateDepartmentList();
            ViewBag.CountryList = _filterRepository.CreateCountryList();
            ViewBag.CityList = _filterRepository.CreateCityList();
            ViewBag.JobList = _filterRepository.CreateJobList();
            ViewBag.PhoneNmbList = _filterRepository.CreatePhoneNumberList();
            ViewBag.EmployeeList = _filterRepository.CreateEmployeeList();

            return View(modelsForEmployee);
        }

        [HttpGet("employee-info/{employeeId:int}")]
        public IActionResult UserInfo(int employeeId)
        {
            var employee = _mapper.Map<EmployeeDto>(_employeeRepository.GetEmployee(employeeId));

            return View(employee);
        }

        [HttpPost]
        [Route("employee-info")]
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
        [Route("salary-report/")]
        public IActionResult SalaryReport()
        {
            ModelForSalaryDto modelForSalary = new ModelForSalaryDto();
            var amountSalaries = _filterSalaryRepository.GetTotalSalary("");

            modelForSalary.Salaries = amountSalaries;
            modelForSalary.FilterRequest = new FilterRequestDto();

            ViewBag.CompanyList = _filterRepository.CreateCompanyList();
            ViewBag.DepartmentList = _filterRepository.CreateDepartmentList();
            ViewBag.CountryList = _filterRepository.CreateCountryList();
            ViewBag.CityList = _filterRepository.CreateCityList();
            ViewBag.JobList = _filterRepository.CreateJobList();
            ViewBag.PhoneNmbList = _filterRepository.CreatePhoneNumberList();
            ViewBag.EmployeeList = _filterRepository.CreateEmployeeList();

            return View(modelForSalary);
        }

        [HttpPost]
        [Route("salary-report/")]
        public IActionResult SalaryReport(FilterRequestDto filterRequest)
        {
            ModelForSalaryDto modelForSalary = new ModelForSalaryDto();

            modelForSalary.Salaries = _filterSalaryRepository
                .GetTotalSalary(_createCommandForFilterSqlQuery.CreateCommandForSql(filterRequest));
            modelForSalary.FilterRequest = new FilterRequestDto()
            {
                CompanyName = filterRequest.CompanyName,
                DepartmentName = filterRequest.DepartmentName,
                CountryName = filterRequest.CountryName,
                CityName = filterRequest.CityName,
                JobName = filterRequest.JobName
            };

            ViewBag.CompanyList = _filterRepository.CreateCompanyList();
            ViewBag.DepartmentList = _filterRepository.CreateDepartmentList();
            ViewBag.CountryList = _filterRepository.CreateCountryList();
            ViewBag.CityList = _filterRepository.CreateCityList();
            ViewBag.JobList = _filterRepository.CreateJobList();
            ViewBag.PhoneNmbList = _filterRepository.CreatePhoneNumberList();
            ViewBag.EmployeeList = _filterRepository.CreateEmployeeList();

            return View(modelForSalary);
        }

        [HttpPost]
        [Route("save-in-file/")]
        public async Task<IActionResult> SaveInFile(FilterRequestDto filterRequest)
        {
            var amountSalaries = _filterSalaryRepository
                .GetTotalSalary(_createCommandForFilterSqlQuery.CreateCommandForSql(filterRequest));

            using (StreamWriter writer = new StreamWriter($"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.txt", true))
            {
                foreach (var item in amountSalaries)
                {
                    await writer.WriteLineAsync($"----------------------------------------------------------");
                    await writer.WriteLineAsync($"| Name: {item.FilterSalaryName} \t|\tAmountSalary: {item.TotalAmountSalary} |");
                    await writer.WriteLineAsync($"----------------------------------------------------------");
                }
            }

            return Redirect("salary-report");
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
    }
}