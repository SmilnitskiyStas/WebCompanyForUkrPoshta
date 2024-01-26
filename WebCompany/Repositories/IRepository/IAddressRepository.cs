using WebCompany.Models;

namespace WebCompany.Repositiories.IRepository
{
    public interface IAddressRepository
    {
        ICollection<Address> GetAddresses();
        Address GetAddress(int id);
        Address GetAddress(string name);
        Address CreateAddress(Address address);
        Address UpdateAddress(Address address);
        bool DeleteAddress(int id);
    }
}
