using AutoMapper;
using Security.Application.Dto.Login;
using Security.Transversal.Auth.Entity;

namespace Security.Transversal.Mapper.Profiler
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<UserToken, LoginResponseDto>()
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token))
                .ReverseMap();
        }
    }
}
