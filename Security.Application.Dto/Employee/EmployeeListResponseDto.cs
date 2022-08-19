using System;
using System.Text.Json.Serialization;

namespace Security.Application.Dto.Employee
{
    public class EmployeeListResponseDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string NameComplete { get; set; }
        public string Division { get; set; }
        public int Grade { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime Birthday { get; set; }
        public string Identification { get; set; }
        [JsonIgnore]
        public decimal BaseSalary { get; set; }
        [JsonIgnore]
        public decimal Commission { get; set; }
        [JsonIgnore]
        public decimal CompensationBonus { get; set; }
        [JsonIgnore]
        public decimal ProductionBonus { get; set; }
        [JsonIgnore]
        public decimal OtherIncome { get; set; }
        [JsonIgnore]
        public decimal Contributions { get; set; }
        [JsonIgnore]
        public DateTime Period { get; set; }
        public decimal TotalSalary { get; set; }

    }
}
