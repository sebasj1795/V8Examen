using Microsoft.AspNetCore.Mvc;
using Security.Application.Dto.Module;
using Security.Application.Dto.Paginate;
using Security.Application.Interfaces;
using Security.Transversal.Common;
using Security.Transversal.Common.Constants;
using Security.Transversal.Common.Enum;
using System.Threading.Tasks;

namespace Distributed.Services.WebApi.Controllers
{
    [Route(RouteConst.Controller)]
    [ApiController]
    public class ModuleController : BaseController
    {
        private readonly IModuleAppService _moduleAppService;
        public ModuleController(IModuleAppService moduleAppService)
        {
            _moduleAppService = moduleAppService;
        }

        /// <summary>
        /// Crear módulo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(Response<ModuleCreateResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> Create(ModuleCreateRequestDto request)
        {
            var result = await _moduleAppService.CreateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Actualizar módulo 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        [ProducesResponseType(typeof(Response<ModuleUpdateResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> Update(ModuleUpdateRequestDto request)
        {
            var result = await _moduleAppService.UpdateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Obtener módulo 
        /// </summary>
        /// <param name="id">Código de menú</param>
        /// <returns></returns>
        [HttpGet("GetById")]
        [ProducesResponseType(typeof(Response<ModuleGetResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var result = await _moduleAppService.GetByIdAsync(id);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Listar módulos 
        /// </summary>
        /// <param name="request">Código de módulo</param>
        /// <returns></returns>
        [HttpPost("GetAll")]
        [ProducesResponseType(typeof(Response<PaginateResponseDto<ModuleListResponseDto>>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> GetAll(PaginateRequestDto request)
        {
            var result = await _moduleAppService.GetAllAsync(request);
            return new OkObjectResult(result);
        }
    }
}
