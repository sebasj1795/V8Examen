using AutoMapper;
using Security.Application.Dto.Employee;
using Security.Domain.Entities;
using System;

namespace Security.Transversal.Mapper.Profiler
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeListResponseDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.EmployeeCode))
               .ForMember(dest => dest.NameComplete, opt => opt.MapFrom(src => src.EmployeeName + " " + src.EmployeeSurname))
               .ForMember(dest => dest.Division, opt => opt.MapFrom(src => src.Division))
               .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade))
               .ForMember(dest => dest.BeginDate, opt => opt.MapFrom(src => src.BeginDate))
               .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
               .ForMember(dest => dest.Identification, opt => opt.MapFrom(src => src.IdentificationNumber))
               .ForMember(dest => dest.BaseSalary, opt => opt.MapFrom(src => src.BaseSalary))
               .ForMember(dest => dest.Commission, opt => opt.MapFrom(src => src.Comission))
               .ForMember(dest => dest.ProductionBonus, opt => opt.MapFrom(src => src.ProductionBonus))
               .ForMember(dest => dest.CompensationBonus, opt => opt.MapFrom(src => src.CompesationBonus))
               .ForMember(dest => dest.Contributions, opt => opt.MapFrom(src => src.Constributions))
               .ForMember(dest => dest.Period, opt => opt.MapFrom(src => new DateTime(src.Year,src.Month,1)));

            CreateMap<EmployeeUpdateRequestDto, Employee>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.EmployeeSurname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Division, opt => opt.MapFrom(src => src.Division))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.BeginDate, opt => opt.MapFrom(src => src.BeginDate))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.IdentificationNumber, opt => opt.MapFrom(src => src.IdentificationNumber))
                .ForMember(dest => dest.BaseSalary, opt => opt.MapFrom(src => src.BaseSalary))
                .ReverseMap();

            CreateMap<EmployeeCreateRequestDto, Employee>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.EmployeeSurname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(dest => dest.Division, opt => opt.MapFrom(src => src.Division))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.Office, opt => opt.MapFrom(src => src.Office))
                .ForMember(dest => dest.BeginDate, opt => opt.MapFrom(src => src.BeginDate))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.IdentificationNumber, opt => opt.MapFrom(src => src.IdentificationNumber))
                .ForMember(dest => dest.BaseSalary, opt => opt.MapFrom(src => src.BaseSalary))
                .ReverseMap();

            CreateMap<Employee, EmployeeGetResponseDto>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.EmployeeCode))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EmployeeName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.EmployeeSurname))
                .ForMember(dest => dest.Division, opt => opt.MapFrom(src => src.Division))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.BeginDate, opt => opt.MapFrom(src => src.BeginDate))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.IdentificationNumber, opt => opt.MapFrom(src => src.IdentificationNumber))
                .ForMember(dest => dest.BaseSalary, opt => opt.MapFrom(src => src.BaseSalary));

            CreateMap<Employee, SalaryEmployeeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Month))
                .ForMember(dest => dest.BaseSalary, opt => opt.MapFrom(src => src.BaseSalary))
                .ForMember(dest => dest.CompensationBonus, opt => opt.MapFrom(src => src.CompesationBonus))
                .ForMember(dest => dest.ProductionBonus, opt => opt.MapFrom(src => src.ProductionBonus))
                .ForMember(dest => dest.Commission, opt => opt.MapFrom(src => src.Comission))
                .ForMember(dest => dest.Contribution, opt => opt.MapFrom(src => src.Constributions))
                .ForMember(dest => dest.Period, opt => opt.MapFrom(src => new DateTime(src.Year, src.Month, 1)))
                .ReverseMap();

            CreateMap<Employee, EmployeePeriodListDto>()
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Month))
                .ForMember(dest => dest.BaseSalary, opt => opt.MapFrom(src => src.BaseSalary))
                .ForMember(dest => dest.CompensationBonus, opt => opt.MapFrom(src => src.CompesationBonus))
                .ForMember(dest => dest.ProductionBonus, opt => opt.MapFrom(src => src.ProductionBonus))
                .ForMember(dest => dest.Commission, opt => opt.MapFrom(src => src.Comission))
                .ForMember(dest => dest.Contribution, opt => opt.MapFrom(src => src.Constributions))
                .ForMember(dest => dest.Period, opt => opt.MapFrom(src => new DateTime(src.Year, src.Month, 1)));

        }
    }
}
