
using Microsoft.AspNetCore.Mvc;
using Security.Application.Dto.Paginate;
using Security.Application.Dto.User;
using Security.Application.Interfaces;
using Security.Transversal.Common;
using Security.Transversal.Common.Constants;
using Security.Transversal.Common.Enum;
using System.Threading.Tasks;

namespace Distributed.Services.WebApi.Controllers
{
    [Route(RouteConst.Controller)]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserAppService _userAppService;
        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// Crear usuario 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [ProducesResponseType(typeof(Response<UserCreateResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> Create(UserCreateRequestDto request)
        {
            var result = await _userAppService.CreateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Actualizar usuario 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        [ProducesResponseType(typeof(Response<UserUpdateResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> Update(UserUpdateRequestDto request)
        {
            var result = await _userAppService.UpdateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Obtener usuario 
        /// </summary>
        /// <param name="id">Código de usuario</param>
        /// <returns></returns>
        [HttpGet("GetById")]
        [ProducesResponseType(typeof(Response<UserGetResponseDto>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) {
                return NotFound();
            }
            var result = await _userAppService.GetByIdAsync(id);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Listar usuarios
        /// </summary>
        /// <param name="request">Código de usuario</param>
        /// <returns></returns>
        [HttpPost("GetAll")]
        [ProducesResponseType(typeof(Response<PaginateResponseDto<UserListResponseDto>>), (int)StatusHttpEnum.Ok)]
        public async Task<IActionResult> GetAll(PaginateRequestDto request)
        {
            var result = await _userAppService.GetAllAsync(request);
            return new OkObjectResult(result);
        }

    }
}
