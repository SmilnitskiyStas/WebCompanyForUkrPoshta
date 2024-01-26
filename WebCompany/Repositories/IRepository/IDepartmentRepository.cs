using WebCompany.Models;

namespace WebCompany.Repositiories.IRepository
{
    public interface IDepartmentRepository
    {
        ICollection<Department> GetDepartments();
        Department GetDepartment(int id);
        Department GetDepartment(string name);
        Department CreateDepartment(Department department);
        Department UpdateDepartment(Department department);
        bool DeleteDepartment(int id);
    }
}
