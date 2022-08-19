using Microsoft.AspNetCore.Mvc;
using Security.Application.Dto.Menu;
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
    public class MenuController : BaseController
    {
        private readonly IMenuAppService _menuAppService;
        public MenuController(IMenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }

        /// <summary>
        /// Crear Menú 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(Response<MenuCreateResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> Create(MenuCreateRequestDto request)
        {
            var result = await _menuAppService.CreateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Actualizar menú 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        [ProducesResponseType(typeof(Response<MenuUpdateResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> Update(MenuUpdateRequestDto request)
        {
            var result = await _menuAppService.UpdateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Obtener menú 
        /// </summary>
        /// <param name="id">Código de menú</param>
        /// <returns></returns>
        [HttpGet("GetById")]
        [ProducesResponseType(typeof(Response<MenuGetResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var result = await _menuAppService.GetByIdAsync(id);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Listar menús 
        /// </summary>
        /// <param name="request">datos paginación</param>
        /// <returns></returns>
        [HttpPost("GetAll")]
        [ProducesResponseType(typeof(Response<PaginateResponseDto<MenuListResponseDto>>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> GetAll(PaginateRequestDto request)
        {
            var result = await _menuAppService.GetAllAsync(request);
            return new OkObjectResult(result);
        }


    }
}
