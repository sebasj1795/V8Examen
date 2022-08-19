using AutoMapper;
using Security.Application.Dto.App;
using Security.Domain.Entities;

namespace Security.Transversal.Mapper.Profiler
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            #region GET
            CreateMap<App, AppGetResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.Server, opt => opt.MapFrom(src => src.Server))
                .ForMember(dest => dest.UserServer, opt => opt.MapFrom(src => src.UserServer))
                .ForMember(dest => dest.PasswordServer, opt => opt.MapFrom(src => src.PasswordServer))
                .ForMember(dest => dest.Port, opt => opt.MapFrom(src => src.Port))
                .ForMember(dest => dest.NameBd, opt => opt.MapFrom(src => src.NameBd))
                .ForMember(dest => dest.Platform, opt => opt.MapFrom(src => src.Platform))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ReverseMap();
            #endregion

            #region Create
            CreateMap<AppCreateRequestDto, App>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
               .ReverseMap();

            CreateMap<App, AppCreateResponseDto>()
                .ReverseMap();
            #endregion

            #region Update
            CreateMap<AppUpdateRequestDto, App>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<App, AppUpdateResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
            #endregion

            #region List
            CreateMap<App, AppListResponseDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
               .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));
            #endregion

        }
    }
}
