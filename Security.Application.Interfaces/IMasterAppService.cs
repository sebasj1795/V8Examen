using Security.Application.Dto.Master;
using Security.Transversal.Common;
using System.Threading.Tasks;

namespace Security.Application.Interfaces
{
    public interface IMasterAppService
    {
        Task<Response<MasterCreateRequestDto>> CreateDemoAsync(MasterCreateRequestDto request);
    }
}
