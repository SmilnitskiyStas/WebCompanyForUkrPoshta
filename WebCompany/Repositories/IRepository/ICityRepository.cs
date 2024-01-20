using WebCompany.Models;

namespace WebCompany.Repositiories.IRepository
{
    public interface ICityRepository
    {
        ICollection<City> GetCities();
        City GetCity(int id);
        City GetCity(string cityName);
        City CreateCity(City city);
        City UpdateCity(City city);
        bool DeleteCity(int id);
    }
}
