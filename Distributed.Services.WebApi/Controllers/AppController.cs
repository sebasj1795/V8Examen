using Microsoft.AspNetCore.Mvc;
using Security.Application.Dto.App;
using Security.Application.Dto.Paginate;
using Security.Application.Interfaces;
using Security.Transversal.Common;
using Security.Transversal.Common.Constants;
using Security.Transversal.Common.Enum;
using System.Threading.Tasks;

namespace Distributed.Services.WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route(RouteConst.Controller)]
    [ApiController]
    public class AppController : BaseController
    {
        private readonly IAppAppService _appAppService;
        public AppController(IAppAppService appAppService)
        {
            _appAppService = appAppService;
        }

        /// <summary>
        /// Crear App 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(Response<AppCreateResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> Create(AppCreateRequestDto request)
        {
            var result = await _appAppService.CreateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Actualizar App 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        [ProducesResponseType(typeof(Response<AppUpdateResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> Update(AppUpdateRequestDto request)
        {
            var result = await _appAppService.UpdateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Obtener app 
        /// </summary>
        /// <param name="id">Código de app</param>
        /// <returns></returns>
        [HttpGet("GetById")]
        [ProducesResponseType(typeof(Response<AppGetResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return NotFound();

            var result = await _appAppService.GetByIdAsync(id);

            if (result.Data is null && result.Code == (int)CodeResponseEnum.Success)
                return NotFound();

            return new OkObjectResult(result);
        }

        /// <summary>
        /// Listar Apps 
        /// </summary>
        /// <param name="request">Código de App</param>
        /// <returns></returns>
        [HttpPost("GetAll")]
        [ProducesResponseType(typeof(Response<PaginateResponseDto<AppListResponseDto>>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> GetAll(PaginateRequestDto request)
        {
            var result = await _appAppService.GetAllAsync(request);
            return new OkObjectResult(result);
        }
    }
}
