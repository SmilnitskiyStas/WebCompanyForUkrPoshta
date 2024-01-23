namespace WebCompany.Models.Dto
{
    public class ModelForEmployeeDto
    {
        public List<EmployeeDto> Employees { get; set; }
        public FilterRequestDto FilterRequest { get; set; }
    }
}
