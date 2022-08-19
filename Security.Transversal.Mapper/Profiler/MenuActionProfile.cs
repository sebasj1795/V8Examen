using AutoMapper;
using Security.Application.Dto.MenuAction;
using Security.Domain.Entities;

namespace Security.Transversal.Mapper.Profiler
{
    public class MenuActionProfile : Profile
    {
        public MenuActionProfile()
        {
            #region GET
            CreateMap<MenuAction, MenuActionGetResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdMenu, opt => opt.MapFrom(src => src.IdMenu))
                .ForMember(dest => dest.IdAction, opt => opt.MapFrom(src => src.IdAction))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ReverseMap();
            #endregion

            #region Create
            CreateMap<MenuActionCreateRequestDto, MenuAction>()
               .ForMember(dest => dest.IdMenu, opt => opt.MapFrom(src => src.IdMenu))
               .ForMember(dest => dest.IdAction, opt => opt.MapFrom(src => src.IdAction))
               .ReverseMap();

            CreateMap<MenuAction, MenuActionCreateResponseDto>()
                .ReverseMap();
            #endregion

            #region Update
            CreateMap<MenuActionUpdateRequestDto, MenuAction>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdMenu, opt => opt.MapFrom(src => src.IdMenu))
                .ForMember(dest => dest.IdAction, opt => opt.MapFrom(src => src.IdAction))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ReverseMap();

            CreateMap<MenuAction, MenuActionUpdateResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdMenu, opt => opt.MapFrom(src => src.IdMenu))
                .ForMember(dest => dest.IdAction, opt => opt.MapFrom(src => src.IdAction))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ReverseMap();
            #endregion

        }
    }
}
