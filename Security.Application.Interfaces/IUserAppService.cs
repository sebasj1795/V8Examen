using Security.Application.Dto.Paginate;
using Security.Application.Dto.User;
using Security.Transversal.Common;
using Security.Transversal.Common.Paginate;
using System.Threading.Tasks;

namespace Security.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<Response<UserCreateResponseDto>> CreateAsync(UserCreateRequestDto request);
        Task<Response<UserUpdateResponseDto>> UpdateAsync(UserUpdateRequestDto request);
        Task<Response<UserGetResponseDto>> GetByIdAsync(int id);
        Task<Response<PaginateResponseDto<UserListResponseDto>>> GetAllAsync(PaginateRequestDto request);
    }
}
