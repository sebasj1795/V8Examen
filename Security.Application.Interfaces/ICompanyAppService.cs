using Security.Application.Dto.Company;
using Security.Application.Dto.Paginate;
using Security.Transversal.Common;
using System.Threading.Tasks;

namespace Security.Application.Interfaces
{
    public interface ICompanyAppService
    {
        Task<Response<CompanyCreateResponseDto>> CreateAsync(CompanyCreateRequestDto request);
        Task<Response<CompanyUpdateResponseDto>> UpdateAsync(CompanyUpdateRequestDto request);
        Task<Response<CompanyGetResponseDto>> GetByIdAsync(int id);
        Task<Response<PaginateResponseDto<CompanyListResponseDto>>> GetAllAsync(PaginateRequestDto request);
    }
}
