using AutoMapper;
using WebCompany.Models;
using WebCompany.Models.Dto;

namespace WebCompany.Helpers
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>();
            CreateMap<City, CityDto>();
            CreateMap<CityDto, City>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Job, JobDto>();
            CreateMap<JobDto, Job>();
            CreateMap<PhoneNmb, PhoneNmbDto>();
            CreateMap<PhoneNmbDto, PhoneNmb>();
        }
    }
}
