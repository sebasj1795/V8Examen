using AutoMapper;
using Security.Application.Dto.Permission;
using Security.Domain.Entities;

namespace Security.Transversal.Mapper.Profiler
{
    public class PermisoProfile : Profile
    {
        public PermisoProfile()
        {
            CreateMap<User, PermisoUserDto>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
               //.ForAllOtherMembers(x => x.Ignore());

            CreateMap<UserRole, PermisoRolDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Name));
            //.ForAllOtherMembers(x => x.Ignore());

            CreateMap<MenuRole, PermisoMenuRoleDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.IdRole, opt => opt.MapFrom(src => src.IdRole))
               .ForMember(dest => dest.IdMenuAction, opt => opt.MapFrom(src => src.IdMenuAction))
               .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));
            //.ForAllOtherMembers(x => x.Ignore());

            CreateMap<MenuAction, PermisoMenuActionDto>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.IdMenu, opt => opt.MapFrom(src => src.IdMenu))
              .ForMember(dest => dest.IdAction, opt => opt.MapFrom(src => src.IdAction))
              .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));
            //.ForAllOtherMembers(x => x.Ignore());

            CreateMap<Menu, PermisoMenuDto>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
              .ForMember(dest => dest.IdParent, opt => opt.MapFrom(src => src.IdParent))
              .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level));
            //.ForAllOtherMembers(x => x.Ignore());

            CreateMap<Action, PermisoActionDto>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            //.ForAllOtherMembers(x => x.Ignore());

            #region Menu Level
            //
            CreateMap<Menu, PermisoMenuLevelGenericDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.IdParent, opt => opt.MapFrom(src => src.IdParent))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
            .ForMember(dest => dest.IsForm, opt => opt.MapFrom(src => src.IsForm))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
            .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level));
            //.ForAllOtherMembers(x => x.Ignore());

            CreateMap<PermisoMenuLevelGenericDto, PermisoMenuLevel1Dto>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.IdParent, opt => opt.MapFrom(src => src.IdParent))
             .ForMember(dest => dest.IsForm, opt => opt.MapFrom(src => src.IsForm))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
             .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level));
            //.ForAllOtherMembers(x => x.Ignore());

            CreateMap<PermisoMenuLevelGenericDto, PermisoMenuLevel2Dto>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.IdParent, opt => opt.MapFrom(src => src.IdParent))
             .ForMember(dest => dest.IsForm, opt => opt.MapFrom(src => src.IsForm))
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
             .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level));
            //.ForAllOtherMembers(x => x.Ignore());

            CreateMap<PermisoMenuLevelGenericDto, PermisoMenuLevel3Dto>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.IdParent, opt => opt.MapFrom(src => src.IdParent))
             .ForMember(dest => dest.IsForm, opt => opt.MapFrom(src => src.IsForm))
             .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
             .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level));
          //.ForAllOtherMembers(x => x.Ignore());
            #endregion

            #region Menus - para que no rastree valores de otros menus
            CreateMap<PermisoMenuLevel1Dto, PermisoMenuLevel1Dto>()
            .ReverseMap();

            CreateMap<PermisoMenuLevel2Dto, PermisoMenuLevel2Dto>()
            .ReverseMap();

            CreateMap<PermisoMenuLevel3Dto, PermisoMenuLevel3Dto>()
            .ReverseMap();

            CreateMap<PermisoMenuLevel4Dto, PermisoMenuLevel4Dto>()
           .ReverseMap();
            #endregion

        }
    }
}
