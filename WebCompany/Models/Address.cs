namespace WebCompany.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Street { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
    }
}
