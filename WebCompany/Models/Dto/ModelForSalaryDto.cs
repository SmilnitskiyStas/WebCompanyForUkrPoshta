namespace WebCompany.Models.Dto
{
    public class ModelForSalaryDto
    {
        public ICollection<FilterSalaryDto> Salaries { get; set; }
        public FilterRequestDto FilterRequest { get; set; }
    }
}
