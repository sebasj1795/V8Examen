using Microsoft.AspNetCore.Mvc;
using Security.Application.Dto.MasterDet;
using Security.Application.Dto.Paginate;
using Security.Application.Interfaces;
using Security.Transversal.Common.Constants;
using System.Threading.Tasks;

namespace Distributed.Services.WebApi.Controllers
{
    [Route(RouteConst.Controller)]
    [ApiController]
    public class MasterDetController : BaseController
    {
        private readonly IMasterDetAppService _masterDetAppService;
        public MasterDetController(IMasterDetAppService masterDetAppService)
        {
            _masterDetAppService = masterDetAppService;
        }

        /// <summary>
        /// Crear Maestro detalle 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(MasterDetCreateRequestDto request)
        {
            var result = await _masterDetAppService.CreateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Actualizar Maestro detalle 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update(MasterDetUpdateRequestDto request)
        {
            var result = await _masterDetAppService.UpdateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Obtener maestro detalle 
        /// </summary>
        /// <param name="id">Código de maestro detalle</param>
        /// <returns></returns>
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return NotFound();

            var result = await _masterDetAppService.GetByIdAsync(id);

            if (result.Data is null)
                return NotFound();

            return new OkObjectResult(result);
        }

        /// <summary>
        /// Listar Maestros Detalles 
        /// </summary>
        /// <param name="request">datos paginación</param>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PaginateRequestDto request)
        {
            var result = await _masterDetAppService.GetAllAsync(request);
            return new OkObjectResult(result);
        }


    }
}
