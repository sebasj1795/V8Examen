using AutoMapper;
using Security.Application.Dto.Master;
using Security.Domain.Entities;

namespace Security.Transversal.Mapper.Profiler
{
    public class MasterProfile : Profile
    {
        public MasterProfile()
        {
            CreateMap<MasterCreateRequestDto, Master>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
