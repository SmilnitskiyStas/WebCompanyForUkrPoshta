using WebCompany.Models;
using WebCompany.Models.Dto;

namespace WebCompany.Repositiories.IRepository
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> GetEmployees();
        ICollection<Employee> GetEmployeesOfFilters(string filters);
        Employee GetEmployee(int id);
        Employee GetEmployee(string employeeName);
        Employee CreateEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        bool DeleteEmployee(int id);
    }
}
