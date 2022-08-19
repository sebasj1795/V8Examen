using Microsoft.AspNetCore.Mvc;
using Security.Application.Dto.MenuAction;
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
    public class MenuActionController : BaseController
    {
        private readonly IMenuActionAppService _menuActionAppService;
        public MenuActionController(IMenuActionAppService menuActionAppService)
        {
            _menuActionAppService = menuActionAppService;
        }

        /// <summary>
        /// Crear Acción de Menú 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(Response<MenuActionCreateResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> Create(MenuActionCreateRequestDto request)
        {
            var result = await _menuActionAppService.CreateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Actualizar Acción de menú 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        [ProducesResponseType(typeof(Response<MenuActionUpdateResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> Update(MenuActionUpdateRequestDto request)
        {
            var result = await _menuActionAppService.UpdateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Obtener menú acción 
        /// </summary>
        /// <param name="id">Código de Menu Acción</param>
        /// <returns></returns>
        [HttpGet("GetById")]
        [ProducesResponseType(typeof(Response<MenuActionGetResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var result = await _menuActionAppService.GetByIdAsync(id);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Listar Acciones de menú 
        /// </summary>
        /// <param name="request">Paginación</param>
        /// <returns></returns>
        [HttpPost("GetAll")]
        [ProducesResponseType(typeof(Response<PaginateResponseDto<MenuActionListResponseDto>>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> GetAll(PaginateRequestDto request)
        {
            var result = await _menuActionAppService.GetAllAsync(request);
            return new OkObjectResult(result);
        }


    }
}
