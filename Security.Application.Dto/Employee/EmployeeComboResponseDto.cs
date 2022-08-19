using Security.Application.Dto.Base;
using System.Collections.Generic;

namespace Security.Application.Dto.Employee
{
    public class EmployeeComboResponseDto
    {
        public EmployeeComboResponseDto()
        {
            Divisions = new List<ItemDto>();
            Positions = new List<ItemDto>();
            Offices = new List<ItemDto>();
        }
        public List<ItemDto> Divisions { get; set; }
        public List<ItemDto> Positions { get; set; }
        public List<ItemDto> Offices { get; set; }
    }
}
