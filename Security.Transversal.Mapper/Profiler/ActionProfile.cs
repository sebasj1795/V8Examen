using AutoMapper;
using Security.Application.Dto.Action;
using Security.Domain.Entities;

namespace Security.Transversal.Mapper.Profiler
{
    public class ActionProfile : Profile
    {
        public ActionProfile()
        {

            #region GET
            CreateMap<Action, ActionGetResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ReverseMap();
            #endregion

            #region Create
            CreateMap<ActionCreateRequestDto, Action>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Action, ActionCreateResponseDto>()
            .ReverseMap();

            #endregion

            #region Update
            CreateMap<ActionUpdateRequestDto, Action>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<Action, ActionUpdateResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
            #endregion

            #region List
            CreateMap<Action, ActionListResponseDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
               .ForMember(dest => dest.UserCrea, opt => opt.MapFrom(src => src.UserCrea))
               .ForMember(dest => dest.DateCrea, opt => opt.MapFrom(src => src.DateCrea))
               .ForMember(dest => dest.UserUpd, opt => opt.MapFrom(src => src.UserUpd))
               .ForMember(dest => dest.DateUpd, opt => opt.MapFrom(src => src.DateUpd));
            #endregion

        }
    }
}
