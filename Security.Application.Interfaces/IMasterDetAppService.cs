using Security.Application.Dto.Master;
using Security.Application.Dto.MasterDet;
using Security.Application.Dto.Paginate;
using Security.Transversal.Common;
using System.Threading.Tasks;

namespace Security.Application.Interfaces
{
    public interface IMasterDetAppService
    {
        Task<Response<MasterDetCreateResponseDto>> CreateAsync(MasterDetCreateRequestDto request);
        Task<Response<MasterDetUpdateResponseDto>> UpdateAsync(MasterDetUpdateRequestDto request);
        Task<Response<MasterDetGetResponseDto>> GetByIdAsync(int id);
        Task<Response<PaginateResponseDto<MasterDetListResponseDto>>> GetAllAsync(PaginateRequestDto request);
    }
}
