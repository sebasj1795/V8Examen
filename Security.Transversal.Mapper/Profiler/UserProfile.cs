using AutoMapper;
using Security.Application.Dto.User;
using Security.Domain.Entities;

namespace Security.Transversal.Mapper.Profiler
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserGetResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ReverseMap();

            CreateMap<User,Auth.Entity.User >()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                //.ForMember(dest => dest.RolId, opt => opt.MapFrom(src => src.IdRol))
                .ReverseMap();

            #region Create
            CreateMap<UserCreateRequestDto, User>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
               .ForMember(dest => dest.Expire, opt => opt.MapFrom(src => src.Expire))
               .ReverseMap();

            CreateMap<User, UserCreateResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Expire, opt => opt.MapFrom(src => src.Expire))
                //.ForMember(dest => dest.IdRol, opt => opt.MapFrom(src => src.IdRol))
                .ReverseMap();
            #endregion

            #region Update
            CreateMap<UserUpdateRequestDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.State, opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirm, opt => opt.Ignore())
                .ForMember(dest => dest.ChangePassword, opt => opt.Ignore())
                .ForMember(dest => dest.NumberAttempt, opt => opt.Ignore())
                .ForMember(dest => dest.DateAttempt, opt => opt.Ignore())
                .ForMember(dest => dest.ModeAuthentication, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<User, UserUpdateResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Expire, opt => opt.MapFrom(src => src.Expire))
                //.ForMember(dest => dest.IdRol, opt => opt.MapFrom(src => src.IdRol))
                .ReverseMap();
            #endregion

            #region List
            CreateMap<User, UserListResponseDto>()
                .ReverseMap();
            #endregion
        }
    }
}
