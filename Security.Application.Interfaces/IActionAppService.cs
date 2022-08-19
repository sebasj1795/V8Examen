using Security.Application.Dto.Action;
using Security.Application.Dto.Paginate;
using Security.Transversal.Common;
using System.Threading.Tasks;

namespace Security.Application.Interfaces
{
    public interface IActionAppService
    {
        Task<Response<ActionCreateResponseDto>> CreateAsync(ActionCreateRequestDto request);
        Task<Response<ActionUpdateResponseDto>> UpdateAsync(ActionUpdateRequestDto request);
        Task<Response<ActionGetResponseDto>> GetByIdAsync(int id);
        Task<Response<PaginateResponseDto<ActionListResponseDto>>> GetAllAsync(PaginateRequestDto request);
    }
}
