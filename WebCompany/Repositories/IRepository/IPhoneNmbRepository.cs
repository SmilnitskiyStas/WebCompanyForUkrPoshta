using WebCompany.Models;

namespace WebCompany.Repositiories.IRepository
{
    public interface IPhoneNmbRepository
    {
        ICollection<PhoneNmb> GetPhones();
        PhoneNmb GetPhone(int id);
        PhoneNmb GetPhone(string phoneNumber);
        PhoneNmb CreatePhoneNmb(PhoneNmb phoneNmb);
        PhoneNmb UpdatePhoneNmb(PhoneNmb phoneNmb);
        bool DeletePhoneNmb(int id);
    }
}
