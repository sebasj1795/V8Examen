using AutoMapper.QueryableExtensions;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Security.Application.Dto.Base;
using Security.Application.Dto.Base.PrimeNG;
using Security.Application.Dto.Employee;
using Security.Application.Dto.Paginate;
using Security.Application.Interfaces;
using Security.Application.MainModule.Base;
using Security.Application.MainModule.PrimeNG.Helpers;
using Security.Application.MainModule.PrimeNG.Helpers.pagination;
using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Domain.MainModule.Validations;
using Security.Transversal.Common;
using Security.Transversal.Common.Enum;
using Security.Transversal.Common.Helpers;
using Security.Transversal.Common.Paginate;
using Security.Transversal.Common.Paginate.primeNG;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Application.MainModule
{
    public class EmployeeAppService : BaseAppService, IEmployeeAppService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeAppService(IServiceProvider serviceProvider,
            IEmployeeRepository employeeRepository) : base(serviceProvider)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Response<EmployeeGetResponseDto>> CreateAsync(EmployeeCreateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<EmployeeGetResponseDto>();

            await UnitOfWork.BeginTransactionAsync();

            if (!request.LstSalary.Any())
                response = TypeMessageHelper.MessageWarning<EmployeeGetResponseDto>("Debe tener al menos un periodo");

            string lastCode = _employeeRepository.Find(x => x.Id != 0).OrderByDescending(x => x.EmployeeCode).Select(x => x.EmployeeCode).FirstOrDefault();
            if (lastCode == null)
                lastCode = Convert.ToString(8).PadLeft(8, '0');
            else lastCode = (Convert.ToInt32(lastCode) + 1).ToString();

            ValidationResult resultValidate = new ValidationResult();
            foreach (var period in request.LstSalary)
            {
                var employeeEntity = Mapper.Map<Employee>(request);
                employeeEntity.EmployeeCode = lastCode;
                employeeEntity.Year = period.Period.Year;
                employeeEntity.Month = period.Period.Month;
                employeeEntity.BaseSalary = period.BaseSalary;
                employeeEntity.Comission = period.Commission;
                employeeEntity.CompesationBonus = period.CompensationBonus;
                employeeEntity.ProductionBonus = period.ProductionBonus;
                employeeEntity.Constributions = period.Contribution;

                resultValidate = _employeeRepository.AddAsync(employeeEntity, new EmployeeValidator(_employeeRepository, (int)ActionCrudEnum.Create)).Result;
                if (!resultValidate.IsValid) break;
            }

            if (resultValidate.IsValid)
            {
                await UnitOfWork.CommitTransactionAsync();
                var employee = await GetByIdAsync(lastCode);
                response.Data = employee.Data;
                return response;
            }
            else
                response = TypeMessageHelper.MessageWarning<EmployeeGetResponseDto>(resultValidate.ToString("\n"));

            return response;
        }

        public async Task<Response<EmployeeGetResponseDto>> UpdateAsync(EmployeeUpdateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<EmployeeGetResponseDto>();

            await UnitOfWork.BeginTransactionAsync();

            var entities = await _employeeRepository.Find(employee => employee.EmployeeCode == request.Code, false).ToListAsync();

            ValidationResult resultValidate = new ValidationResult();
            foreach (var periodDto in request.LstSalary)
            {
                var entity = entities.Where(x => x.Year == periodDto.Year && x.Month == periodDto.Month).FirstOrDefault();
                if (entity != null)
                {
                    entity.EmployeeName = request.Name;
                    entity.EmployeeSurname = request.Surname;
                    entity.Division = request.Division;
                    entity.Position = request.Position;
                    entity.Grade = request.Grade;
                    entity.Office = request.Office;
                    entity.BeginDate = request.BeginDate;
                    entity.Birthday = request.Birthday;
                    entity.IdentificationNumber = request.IdentificationNumber;
                    entity.BaseSalary = periodDto.BaseSalary;
                    entity.ProductionBonus = periodDto.ProductionBonus;
                    entity.CompesationBonus = periodDto.CompensationBonus;
                    entity.Comission = periodDto.Commission;
                    entity.Constributions = periodDto.Contribution;

                    resultValidate = new EmployeeValidator(_employeeRepository, (int)ActionCrudEnum.Update).Validate(entity);
                    if (!resultValidate.IsValid) break;
                }
                else
                {
                    var newEntity = new Employee()
                    {
                        Year = periodDto.Year,
                        Month = periodDto.Month,
                        EmployeeCode = request.Code,
                        EmployeeName = request.Name,
                        EmployeeSurname = request.Surname,
                        Division = request.Division,
                        Position = request.Position,
                        Grade = request.Grade,
                        Office = request.Office,
                        BeginDate = request.BeginDate,
                        Birthday = request.Birthday,
                        IdentificationNumber = request.IdentificationNumber,
                        BaseSalary = request.BaseSalary,
                        ProductionBonus = periodDto.ProductionBonus,
                        CompesationBonus = periodDto.CompensationBonus,
                        Comission = periodDto.Commission,
                        Constributions = periodDto.Contribution
                };
                    resultValidate = await _employeeRepository.AddAsync(newEntity, new EmployeeValidator(_employeeRepository, (int)ActionCrudEnum.Update));
                    if (!resultValidate.IsValid) break;
                }
            }

            if (resultValidate.IsValid)
            {
                await UnitOfWork.CommitTransactionAsync();
                var employee = await GetByIdAsync(request.Code);
                response.Data = employee.Data;
                return response;
            }

            response = TypeMessageHelper.MessageWarning<EmployeeGetResponseDto>(resultValidate.ToString("\n"));
            return response;
        }

        public async Task<Response<EmployeeGetResponseDto>> GetByIdAsync(string code)
        {
            var response = TypeMessageHelper.MessageSuccess<EmployeeGetResponseDto>();
            var entities = await _employeeRepository.Find(x => x.EmployeeCode == code).ToListAsync();

            var employeeDto = new EmployeeGetResponseDto();
            if (entities.Any())
            {
                employeeDto = Mapper.Map<EmployeeGetResponseDto>(entities.FirstOrDefault());
                employeeDto.LstSalary = Mapper.Map<List<SalaryEmployeeDto>>(entities);

                setTotalRowSalary(employeeDto.LstSalary);

                #region Get Last Date
                var lastPeriod = employeeDto.LstSalary.OrderByDescending(x => x.Period).First();
                int lastDay = DateTime.DaysInMonth(lastPeriod.Year, lastPeriod.Month);
                employeeDto.EndDate = new DateTime(lastPeriod.Year, lastPeriod.Month, lastDay);
                #endregion

                #region Color yellow number sequency
                setColorYellowPeriodSequency(employeeDto.LstSalary);
                #endregion

                employeeDto.Bonus = sumBonusSalary(employeeDto.LstSalary);

                if (employeeDto.LstSalary.Any()) employeeDto.LstSalary = employeeDto.LstSalary.OrderBy(x => x.Period).ToList();
            }
            response.Data = employeeDto;
            return response;
        }

        private bool setColorYellowPeriodSequency(List<SalaryEmployeeDto> lstSalary)
        {
            lstSalary = lstSalary.Where(x=>x.Id>0).OrderByDescending(x => x.Period).Take(3).ToList();
            bool isColorYellow;
            var ThreeLast = lstSalary.Select(x => x.Month).ToArray();
            ThreeLast = ThreeLast.OrderBy(x => x).ToArray();
            isColorYellow = (ThreeLast.Zip(ThreeLast.Skip(1), (a, b) => (a + 1) == b).All(x => x) && ThreeLast.Count() == 3);

            if (isColorYellow)
            {
                lstSalary.ForEach(x =>
                {
                    if (x.Id > 0)
                    {
                        x.IsColorYellow = true;
                    }
                });
            }
            return isColorYellow;
        }

        private decimal sumBonusSalary(List<SalaryEmployeeDto> lstSalary)
        {
            decimal sumBonus = 0;
            var lstTake = lstSalary.Where(x=>x.Id>0).OrderByDescending(x => x.Period).Take(3).ToList();
            if (lstTake.Any()) {
                int[] ThreeLastMonth = lstTake.Select(x => x.Month).ToArray();
                ThreeLastMonth = ThreeLastMonth.OrderBy(x => x).ToArray();

                int divisor = 3;
                bool isTreeSequency = (ThreeLastMonth.Zip(ThreeLastMonth.Skip(1), (a, b) => (a + 1) == b).All(x => x) && ThreeLastMonth.Count() == 3);
                if (isTreeSequency)
                {
                    sumBonus = lstTake.Sum(x => x.Total) / divisor;
                }
                else
                {
                    ThreeLastMonth = ThreeLastMonth.OrderByDescending(x=>x).ToArray();
                    int[] TwoArray = ThreeLastMonth.Take(2).ToArray();
                    TwoArray = TwoArray.OrderBy(x => x).ToArray();
                    bool twoSequecy = (TwoArray.Zip(TwoArray.Skip(1), (a, b) => (a + 1) == b).All(x => x) && TwoArray.Count() == 2);
                    if (twoSequecy)
                    {
                        sumBonus = lstTake.Take(2).Sum(x => x.Total) / divisor;
                    }
                    else
                    {
                        decimal salaryBase = lstTake.Take(1).Select(x => x.Total).First();
                        sumBonus = salaryBase / divisor;
                    }
                }
            }
            return Math.Round(sumBonus,2);
        }

        private decimal sumTotalPeriod(decimal baseSalary, decimal productionBonus, decimal compensationBonus, decimal commission, decimal contribution)
        {
            decimal otherIncome = (baseSalary + commission) * (8 / 100) + commission;
            decimal totalSalaryPeriod = baseSalary + productionBonus + (compensationBonus * (75 / 100)) + otherIncome - contribution;
            return totalSalaryPeriod;
        }

        private void setTotalRowSalary(List<SalaryEmployeeDto> lstSalary)
        {
            lstSalary.ForEach(period =>
            {
                period.Total = sumTotalPeriod(period.BaseSalary, period.ProductionBonus, period.CompensationBonus, period.Commission, period.Contribution);
            });
        }

        public async Task<Response<PaginateResponseDto<EmployeeListResponseDto>>> GetAllAsync(EmployeeListRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<PaginateResponseDto<EmployeeListResponseDto>>();

            var parametersDto = PrimeNgToPaginationParameters<EmployeeListResponseDto>.Convert(request.Pagination);
            var parametersDomain =
                parametersDto.ConvertToPaginationParameterDomain<Employee, EmployeeListResponseDto>(Mapper);

            await FilterEmployees(request, parametersDomain);

            var paging = await _employeeRepository.FindAllPagingAsync(parametersDomain);
            var entityDtoList = await paging.Entities
                .ProjectTo<EmployeeListResponseDto>(Mapper.ConfigurationProvider).ToListAsync();

            entityDtoList.ForEach(employee =>
            {
                employee.TotalSalary = sumTotalPeriod(employee.BaseSalary, employee.ProductionBonus, employee.CompensationBonus, employee.Commission, employee.Contributions);
            });

            PaginateResponseDto<EmployeeListResponseDto> paginateResponseDto = new PaginateResponseDto<EmployeeListResponseDto>();
            paginateResponseDto.Entities = entityDtoList;
            paginateResponseDto.Count = paging.TotalCount;
            response.Data = paginateResponseDto;

            return response;
        }

        private async Task FilterEmployees(EmployeeListRequestDto request, PrimeNGPaginateRequest<Employee> parametersDomain)
        {
            #region Filter employee with last period
            var lstEmployeesAll = await _employeeRepository.Find(x => x.Id != 0).Select(employee => new EmployeeFilterMaxDateListDto
            {
                Code = employee.EmployeeCode,
                Period = new DateTime(employee.Year, employee.Month, 1)
            }).ToListAsync();

            var lstEmployeeFilter = lstEmployeesAll.GroupBy(i => i.Code).Select(g => new EmployeeFilterMaxDateListDto
            {
                Code = g.Key,
                Period = g.Max(row => row.Period)
            }).ToList();

            List<int> lstIds = new List<int>();
            lstEmployeeFilter.ForEach(employee =>
            {
                int Id = _employeeRepository.Find(x => x.EmployeeCode == employee.Code && x.Year == employee.Period.Year && x.Month == employee.Period.Month).Select(x => x.Id).First();
                lstIds.Add(Id);
            });
            #endregion
            parametersDomain.WhereFilter =
                    parametersDomain.WhereFilter.AddCondition(x => lstIds.Contains(x.Id));

            if (request.IdEmployeeFilter != 0 && request.TypeFilter != 0)
            {
                var employeeReference = await _employeeRepository.GetAsync(request.IdEmployeeFilter);

                if (request.TypeFilter == (int)EmployeeFilterEnum.MismaOficinaMismoGrado)
                {
                    parametersDomain.WhereFilter =
                    parametersDomain.WhereFilter.AddCondition(x => x.Office == employeeReference.Office && x.Grade == employeeReference.Grade);
                }
                else if (request.TypeFilter == (int)EmployeeFilterEnum.TodasOficinaMismoGrado)
                {
                    parametersDomain.WhereFilter =
                        parametersDomain.WhereFilter.AddCondition(x => x.Grade == employeeReference.Grade);
                }
                else if (request.TypeFilter == (int)EmployeeFilterEnum.MismaPosicionYGrado)
                {
                    parametersDomain.WhereFilter =
                        parametersDomain.WhereFilter.AddCondition(x => x.Position == employeeReference.Position && x.Grade == employeeReference.Grade);
                }
                else if (request.TypeFilter == (int)EmployeeFilterEnum.TodasPosicionesMismoGrado)
                {
                    parametersDomain.WhereFilter =
                        parametersDomain.WhereFilter.AddCondition(x => x.Grade == employeeReference.Grade);
                }
            }
        }

        public async Task<Response<EmployeeComboResponseDto>> GetListComboBox()
        {
            var response = TypeMessageHelper.MessageSuccess<EmployeeComboResponseDto>();

            var divisions = await _employeeRepository.Find(x => x.Id != 0).Select(x => new ItemDto
            {
                Code = x.Division,
                Description = x.Division
            }).Distinct().ToListAsync();

            var positions = await _employeeRepository.Find(x => x.Id != 0).Select(x => new ItemDto
            {
                Code = x.Position,
                Description = x.Position
            }).Distinct().ToListAsync();

            var officess = await _employeeRepository.Find(x => x.Id != 0).Select(x => new ItemDto
            {
                Code = x.Office,
                Description = x.Office
            }).Distinct().ToListAsync();

            EmployeeComboResponseDto result = new EmployeeComboResponseDto();
            result.Divisions = divisions;
            result.Positions = positions;
            result.Offices = officess;
            response.Data = result;

            return response;
        }

        public async Task<Response<List<SalaryEmployeeDto>>> GetListPeriod(EmployeePeriodRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<List<SalaryEmployeeDto>>();

            if (request.EndDate < request.BeginDate)
                return TypeMessageHelper.MessageWarning<List<SalaryEmployeeDto>>(Messages.Employee.EndDate_BeginDate_Invalid);

            if (request.EndDate < DateTime.Now)
                return TypeMessageHelper.MessageWarning<List<SalaryEmployeeDto>>(Messages.Employee.EndDate_DateNow_Invalid);

            List<SalaryEmployeeDto> responseList = new List<SalaryEmployeeDto>();
            if (!string.IsNullOrEmpty(request.CodeEmployee))
            {
                var periods = await _employeeRepository.Find(x => x.EmployeeCode == request.CodeEmployee).ToListAsync();
                if (periods.Any())
                {
                    var listDto = Mapper.Map<List<SalaryEmployeeDto>>(periods);
                    if (listDto.Any())
                    {
                        setTotalRowSalary(listDto);

                        listDto = listDto.OrderBy(x => x.Period).ToList();
                        responseList.AddRange(listDto);
                    }
                }
            }

            DateTime iterator;
            DateTime limit;

            //Validations
            if (request.EndDate > DateTime.Now && request.BeginDate < DateTime.Now)
            {
                request.BeginDate = DateTime.Now;
            }

            DateTime? lastPeriodRegistred = responseList.OrderByDescending(x => x.Period).Select(x => x.Period)?.FirstOrDefault();
            if (lastPeriodRegistred != null)
            {
                if (lastPeriodRegistred > DateTime.Now) {
                    request.BeginDate = lastPeriodRegistred.Value.AddMonths(1);
                }

            }
            //

            if (request.EndDate > request.BeginDate)
            {
                iterator = new DateTime(request.BeginDate.Year, request.BeginDate.Month, 1);
                limit = request.EndDate;
            }
            else
            {
                iterator = new DateTime(request.EndDate.Year, request.EndDate.Month, 1);
                limit = request.BeginDate;
            }

            var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            while (iterator <= limit)
            {
                //dateTimeFormat.GetMonthName(iterator.Month)
                var exist = responseList.Any(x => x.Year == iterator.Year && x.Month == iterator.Month);
                if (!exist)
                {
                    responseList.Add(new SalaryEmployeeDto { Id = 0, Month = iterator.Month, Year = iterator.Year, Period = new DateTime(iterator.Year, iterator.Month, 1) });
                    iterator = iterator.AddMonths(1);
                }
            }
            setColorYellowPeriodSequency(responseList);
            response.Data = responseList;
            return response;
        }
    }
}
