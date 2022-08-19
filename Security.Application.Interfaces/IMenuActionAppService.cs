using Security.Application.Dto.MenuAction;
using Security.Application.Dto.Paginate;
using Security.Transversal.Common;
using System.Threading.Tasks;

namespace Security.Application.Interfaces
{
    public interface IMenuActionAppService
    {
        Task<Response<MenuActionCreateResponseDto>> CreateAsync(MenuActionCreateRequestDto request);
        Task<Response<MenuActionUpdateResponseDto>> UpdateAsync(MenuActionUpdateRequestDto request);
        Task<Response<MenuActionGetResponseDto>> GetByIdAsync(int id);
        Task<Response<PaginateResponseDto<MenuActionListResponseDto>>> GetAllAsync(PaginateRequestDto request);
    }
}
