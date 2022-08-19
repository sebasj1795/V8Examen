using AutoMapper;
using Security.Application.Dto.Menu;
using Security.Domain.Entities;

namespace Security.Transversal.Mapper.Profiler
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {

            #region GET
            CreateMap<Menu, MenuGetResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ReverseMap();
            #endregion

            #region Create
            CreateMap<MenuCreateRequestDto, Menu>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
               .ReverseMap();

            CreateMap<Menu, MenuCreateResponseDto>()
                .ReverseMap();
            #endregion

            #region Update
            CreateMap<MenuUpdateRequestDto, Menu>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<Menu, MenuUpdateResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
            #endregion

            #region List
            CreateMap<Menu, MenuListResponseDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));
            #endregion

        }
    }
}
