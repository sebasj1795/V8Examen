using AutoMapper;
using Security.Application.Dto.Module;
using Security.Domain.Entities;

namespace Security.Transversal.Mapper.Profiler
{
    public class ModuleProfile : Profile
    {
        public ModuleProfile()
        {
            #region GET
            CreateMap<Module, ModuleGetResponseDto>()
                .ReverseMap();
            #endregion

            #region Create
            CreateMap<ModuleCreateRequestDto, Module>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
               .ReverseMap();

            CreateMap<Module, ModuleCreateResponseDto>()
                .ReverseMap();
            #endregion

            #region List
            CreateMap<Module, ModuleListResponseDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.IdApp, opt => opt.MapFrom(src => src.IdApp))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
               .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
               .ForMember(dest => dest.IconCss, opt => opt.MapFrom(src => src.IconCss))
               .ForMember(dest => dest.IconImg, opt => opt.MapFrom(src => src.IconImg))
               .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));
            #endregion

        }
    }
}
