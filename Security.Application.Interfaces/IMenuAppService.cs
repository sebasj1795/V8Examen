using Security.Application.Dto.Menu;
using Security.Application.Dto.Paginate;
using Security.Transversal.Common;
using System.Threading.Tasks;

namespace Security.Application.Interfaces
{
    public interface IMenuAppService
    {
        Task<Response<MenuCreateResponseDto>> CreateAsync(MenuCreateRequestDto request);
        Task<Response<MenuUpdateResponseDto>> UpdateAsync(MenuUpdateRequestDto request);
        Task<Response<MenuGetResponseDto>> GetByIdAsync(int id);
        Task<Response<PaginateResponseDto<MenuListResponseDto>>> GetAllAsync(PaginateRequestDto request);
    }
}
