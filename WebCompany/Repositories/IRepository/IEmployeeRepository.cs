using WebCompany.Models;
using WebCompany.Models.Dto;

namespace WebCompany.Repositiories.IRepository
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> GetEmployees();
        Employee GetEmployee(int id);
        Employee GetEmployee(string employeeName);
        Employee CreateEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        bool DeleteEmployee(int id);
    }
}
