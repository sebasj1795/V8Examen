
using Security.Application.Dto.MasterDet;

namespace Security.Application.Dto.Master
{
    public class MasterCreateRequestDto
    {
        public string Name { get; set; }
        public MasterDetCreateRequestDto masterDet { get; set; }
    }
}
