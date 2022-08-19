using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Security.Application.Dto.Login;
using Security.Application.Interfaces;
using Security.Transversal.Common.Constants;
using System.Threading.Tasks;

namespace Distributed.Services.WebApi.Controllers
{
    [Route(RouteConst.Controller)]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly ILoginAppService _loginAppService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(ILoginAppService loginAppService, ILogger<AuthController> logger)
        {
            _loginAppService = loginAppService;
            _logger = logger;
        }

        /// <summary>
        /// Permite acceso al sistema, se genera un token de acceso
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Access")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var result = await _loginAppService.LoginAsync(request);
            return new OkObjectResult(result);
        }

        /// <summary>
        /// Cambia la contraseña de usuario 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequestDto request)
        {
            var result = await _loginAppService.ChangePasswordAsync(request);
            return new OkObjectResult(result);
        }

    }
}
