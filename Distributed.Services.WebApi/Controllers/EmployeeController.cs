using Microsoft.AspNetCore.Mvc;
using Security.Application.Dto.Base.PrimeNG;
using Security.Application.Dto.Employee;
using Security.Application.Dto.Paginate;
using Security.Application.Interfaces;
using Security.Transversal.Common;
using Security.Transversal.Common.Constants;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Distributed.Services.WebApi.Controllers
{
    [Route(RouteConst.Controller)]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeAppService _employeeAppService;
        public EmployeeController(IEmployeeAppService employeeAppService)
        {
            _employeeAppService = employeeAppService;
        }

        /// <summary>
        /// Create employee
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(Response<EmployeeGetResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create(EmployeeCreateRequestDto request)
        {
            var result = await _employeeAppService.CreateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Update Employee 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        [ProducesResponseType(typeof(Response<EmployeeGetResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(EmployeeUpdateRequestDto request)
        {
            var result = await _employeeAppService.UpdateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Get Employee 
        /// </summary>
        /// <param name="code">Code employee</param>
        /// <returns></returns>
        [HttpGet("GetByCode/{code}")]
        [ProducesResponseType(typeof(Response<EmployeeGetResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById([FromRoute] string code)
        {
            if (string.IsNullOrEmpty(code)) return NotFound();
            var result = await _employeeAppService.GetByIdAsync(code);
            if (result == null) return NotFound();
            return new OkObjectResult(result);
        }

        /// <summary>
        /// List employees
        /// </summary>
        /// <param name="request">Filter and Params of pagination</param>
        /// <returns></returns>
        [HttpPost("GetAll")]
        [ProducesResponseType(typeof(Response<PaginateResponseDto<EmployeeListResponseDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(EmployeeListRequestDto request)
        {
            var result = await _employeeAppService.GetAllAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// List Combos
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetListCombo")]
        [ProducesResponseType(typeof(Response<EmployeeComboResponseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetComboBox()
        {
            var result = await _employeeAppService.GetListComboBox();
            return new OkObjectResult(result);
        }

        /// <summary>
        /// List Period between two dates
        /// </summary>
        /// <param name="request">Params of pagination</param>
        /// <returns></returns>
        [HttpPost("GetListPeriod")]
        [ProducesResponseType(typeof(Response<List<EmployeePeriodListDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetListPeriod(EmployeePeriodRequestDto request)
        {
            var result = await _employeeAppService.GetListPeriod(request);
            return new OkObjectResult(result);
        }

    }
}
