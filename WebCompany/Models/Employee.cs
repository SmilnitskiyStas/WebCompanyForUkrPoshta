namespace WebCompany.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime StartWorkDate { get; set; }
        public decimal Salary_in_UAH { get; set; }
        public int CompanyId { get; set; }
        public int EmployeePhoneId { get; set; }
        public int EmpoyeeAddressId { get; set; }
        public int EmployeeJobId { get; set; }
    }
}
