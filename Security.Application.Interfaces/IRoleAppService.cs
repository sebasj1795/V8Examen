using Security.Application.Dto.Paginate;
using Security.Application.Dto.Role;
using Security.Transversal.Common;
using System.Threading.Tasks;

namespace Security.Application.Interfaces
{
    public interface IRoleAppService
    {
        Task<Response<RoleCreateResponseDto>> CreateAsync(RoleCreateRequestDto request);
        Task<Response<RoleUpdateResponseDto>> UpdateAsync(RoleUpdateRequestDto request);
        Task<Response<RoleGetResponseDto>> GetByIdAsync(int id);
        Task<Response<PaginateResponseDto<RoleListResponseDto>>> GetAllAsync(PaginateRequestDto request);
    }
}
