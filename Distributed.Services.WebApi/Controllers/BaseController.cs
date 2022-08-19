using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Distributed.Services.WebApi.Controllers
{
    [Authorize]
    //[Produces("application/json")]
    public class BaseController : ControllerBase
    {

    }
}
