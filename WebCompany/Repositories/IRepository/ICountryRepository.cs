using WebCompany.Models;

namespace WebCompany.Repositiories.IRepository
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();
        Country GetCountry(int id);
        Country GetCountry(string countryName);
        Country CreateCountry(Country country);
        Country UpdateCountry(Country country);
        bool DeleteCountry(int id);
    }
}
