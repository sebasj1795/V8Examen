using System;

namespace Security.Application.Dto.Employee
{
    public class EmployeePeriodRequestDto
    {
        public string CodeEmployee { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
