using System;
using System.Collections.Generic;

namespace Security.Application.Dto.Employee
{
    public class EmployeeDto
    {
        public EmployeeDto()
        {
            LstSalary = new List<SalaryEmployeeDto>();
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Division { get; set; }
        public string Position { get; set; }
        public int Grade { get; set; }
        public string Office { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime Birthday { get; set; }
        public string IdentificationNumber { get; set; }
        public decimal BaseSalary { get; set; }
        public List<SalaryEmployeeDto> LstSalary { get; set; }
        

    }

    public class SalaryEmployeeDto
    {
        public int Id { get; set; }
        public decimal BaseSalary { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime Period { get; set; }
        public decimal ProductionBonus { get; set; }
        public decimal CompensationBonus { get; set; }
        public decimal Commission { get; set; }
        public decimal Contribution { get; set; }
        public decimal Total { get; set; }
        public bool IsColorYellow { get; set; }
    }

    public class EmployeeTemporary
    {
        public string Code { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }

}
