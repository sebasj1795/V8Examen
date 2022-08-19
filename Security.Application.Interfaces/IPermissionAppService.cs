using Security.Application.Dto.Permission;
using Security.Transversal.Common;
using System.Threading.Tasks;

namespace Security.Application.Interfaces
{
    public interface IPermissionAppService
    {
        Task<Response<PermissionGetResponseDto>> GetByUserAsync();
        Task<Response<PermissionGetResponseDto>> GetByRoleAsync(int IdRole);
    }
}
