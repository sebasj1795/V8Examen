using Security.Application.Dto.Module;
using Security.Application.Dto.Paginate;
using Security.Transversal.Common;
using System.Threading.Tasks;

namespace Security.Application.Interfaces
{
    public interface IModuleAppService
    {
        Task<Response<ModuleCreateResponseDto>> CreateAsync(ModuleCreateRequestDto request);
        Task<Response<ModuleUpdateResponseDto>> UpdateAsync(ModuleUpdateRequestDto request);
        Task<Response<ModuleGetResponseDto>> GetByIdAsync(int id);
        Task<Response<PaginateResponseDto<ModuleListResponseDto>>> GetAllAsync(PaginateRequestDto request);
    }
}
