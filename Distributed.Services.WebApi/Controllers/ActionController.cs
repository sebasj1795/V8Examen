using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Application.Dto.Action;
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
    public class ActionController : BaseController
    {
        private readonly IActionAppService _actionAppService;
        public ActionController(IActionAppService actionAppService)
        {
            _actionAppService = actionAppService;
        }

        /// <summary>
        /// Crear acción 
        /// </summary>
        /// <remarks>
        /// Aquí una descripción mas larga si fuera necesario. Obtiene un objeto por su Id.
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(Response<ActionCreateResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> Create([FromBody] ActionCreateRequestDto request)
        {
            var result = await _actionAppService.CreateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Actualizar acción 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        [ProducesResponseType(typeof(Response<ActionUpdateResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> Update([FromBody]ActionUpdateRequestDto request)
        {
            if (request.Id <= 0)
                return NotFound();

            var result = await _actionAppService.UpdateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Obtener acción 
        /// </summary>
        /// <param name="id">Código de acción</param>
        /// <returns></returns>
        [HttpGet("GetById/{id}")]
        [ProducesResponseType(typeof(Response<ActionGetResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (id <= 0)
                return NotFound();

            var result = await _actionAppService.GetByIdAsync(id);

            if (result.Data is null && result.Code == (int)CodeResponseEnum.Success)
                return NotFound();

            return new OkObjectResult(result);
        }

        /// <summary>
        /// Listar acciones
        /// </summary>
        /// <param name="request">Paginacion</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("GetAll")]
        [ProducesResponseType(typeof(Response<PaginateResponseDto<ActionListResponseDto>>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> GetAll(PaginateRequestDto request)
        {
            var result = await _actionAppService.GetAllAsync(request);
            return new OkObjectResult(result);
        }
    }
}
