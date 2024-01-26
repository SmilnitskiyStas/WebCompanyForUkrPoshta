using WebCompany.Models;

namespace WebCompany.Repositiories.IRepository
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetCompanies();
        Company GetCompany(int id);
        Company GetCompany(string name);
        Company CreateCompany(Company company);
        Company UpdateCompany(Company company);
        bool DeleteCompany(int id);
    }
}
