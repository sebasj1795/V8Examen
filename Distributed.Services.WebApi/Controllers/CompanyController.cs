using Microsoft.AspNetCore.Mvc;
using Security.Application.Dto.Company;
using Security.Application.Dto.Paginate;
using Security.Application.Interfaces;
using Security.Transversal.Common.Constants;
using System.Threading.Tasks;

namespace Distributed.Services.WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route(RouteConst.Controller)]
    [ApiController]
    public class CompanyController : BaseController
    {
        private readonly ICompanyAppService _companyAppService;
        public CompanyController(ICompanyAppService companyAppService)
        {
            _companyAppService = companyAppService;
        }

        /// <summary>
        /// Crear Compañia 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create(CompanyCreateRequestDto request)
        {
            var result = await _companyAppService.CreateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Actualizar Compañia 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update(CompanyUpdateRequestDto request)
        {
            var result = await _companyAppService.UpdateAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Obtener Compañia 
        /// </summary>
        /// <param name="id">Código de Compañia</param>
        /// <returns></returns>
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }
            var result = await _companyAppService.GetByIdAsync(id);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Listar Compañias
        /// </summary>
        /// <param name="request">Parámetros de paginación</param>
        /// <returns></returns>
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(PaginateRequestDto request)
        {
            var result = await _companyAppService.GetAllAsync(request);
            return new OkObjectResult(result);
        }

    }
}
