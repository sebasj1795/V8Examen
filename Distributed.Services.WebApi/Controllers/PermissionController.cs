using Microsoft.AspNetCore.Mvc;
using Security.Application.Interfaces;
using Security.Transversal.Common.Constants;
using System.Threading.Tasks;

namespace Distributed.Services.WebApi.Controllers
{
    [Route(RouteConst.Controller)]
    [ApiController]
    public class PermissionController : BaseController
    {
        private readonly IPermissionAppService _permisoAppService;
        public PermissionController(IPermissionAppService permisoAppService)
        {
            _permisoAppService = permisoAppService;
        }
        /// <summary>
        /// Obtener permisos por usuario 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetByUser()
        {
            var result = await _permisoAppService.GetByUserAsync();
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Obtener permisos por usuario y rol 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByRole")]
        public async Task<IActionResult> GetByRol(int idRol)
        {
            var result = await _permisoAppService.GetByRoleAsync(idRol);
            return new OkObjectResult(result);
        }

    }
}
