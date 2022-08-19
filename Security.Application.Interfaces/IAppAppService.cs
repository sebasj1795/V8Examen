using Security.Application.Dto.App;
using Security.Application.Dto.Paginate;
using Security.Transversal.Common;
using System.Threading.Tasks;

namespace Security.Application.Interfaces
{
    public interface IAppAppService
    {
        Task<Response<AppCreateResponseDto>> CreateAsync(AppCreateRequestDto request);
        Task<Response<AppUpdateResponseDto>> UpdateAsync(AppUpdateRequestDto request);
        Task<Response<AppGetResponseDto>> GetByIdAsync(int id);
        Task<Response<PaginateResponseDto<AppListResponseDto>>> GetAllAsync(PaginateRequestDto request);
    }
}
