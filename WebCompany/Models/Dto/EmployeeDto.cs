namespace WebCompany.Models
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime StartWorkDate { get; set; }
        public decimal Salary_in_UAH { get; set; }
    }
}
