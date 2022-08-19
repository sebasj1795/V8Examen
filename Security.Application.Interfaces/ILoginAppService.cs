using Security.Application.Dto.Login;
using Security.Transversal.Common;
using System.Threading.Tasks;

namespace Security.Application.Interfaces
{
    public interface ILoginAppService
    {
        Task<Response<LoginResponseDto>> LoginAsync(LoginRequestDto request);
        Task<Response> ChangePasswordAsync(ChangePasswordRequestDto request);
    }
}
