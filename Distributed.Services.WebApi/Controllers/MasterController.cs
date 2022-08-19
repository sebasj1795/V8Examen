using Microsoft.AspNetCore.Mvc;
using Security.Application.Dto.Master;
using Security.Application.Interfaces;
using Security.Transversal.Common.Constants;
using System.Threading.Tasks;

namespace Distributed.Services.WebApi.Controllers
{
    [Route(RouteConst.Controller)]
    [ApiController]
    public class MasterController : BaseController
    {
        private readonly IMasterAppService _masterAppService;
        public MasterController(IMasterAppService masterAppService)
        {
            _masterAppService = masterAppService;
        }

        /// <summary>
        /// Crear Maestro detalle 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreateDemo")]
        public async Task<IActionResult> CreateDemo(MasterCreateRequestDto request)
        {
            var result = await _masterAppService.CreateDemoAsync(request);
            return new OkObjectResult(result);
        }

    }
}
