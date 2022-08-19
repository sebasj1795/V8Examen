using Security.Application.Dto.Base.PrimeNG;
using Security.Application.Dto.Employee;
using Security.Application.Dto.Paginate;
using Security.Transversal.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Security.Application.Interfaces
{
    public interface IEmployeeAppService
    {
        Task<Response<EmployeeGetResponseDto>> CreateAsync(EmployeeCreateRequestDto request);
        Task<Response<EmployeeGetResponseDto>> UpdateAsync(EmployeeUpdateRequestDto request);
        Task<Response<EmployeeGetResponseDto>> GetByIdAsync(string code);
        Task<Response<PaginateResponseDto<EmployeeListResponseDto>>> GetAllAsync(EmployeeListRequestDto request);
        Task<Response<EmployeeComboResponseDto>> GetListComboBox();
        Task<Response<List<SalaryEmployeeDto>>> GetListPeriod(EmployeePeriodRequestDto request);
    }
}
