using Security.Application.Dto.Base.PrimeNG;

namespace Security.Application.Dto.Employee
{
    public class EmployeeListRequestDto
    {
        public EmployeeListRequestDto()
        {
            Pagination = new PrimeTableDto();
        }
        public int IdEmployeeFilter { get; set; }
        public int TypeFilter { get; set; }
        public PrimeTableDto Pagination { get; set; }
    }
}
