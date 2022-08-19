using AutoMapper;
using Security.Application.Dto.MasterDet;
using Security.Domain.Entities;

namespace Security.Transversal.Mapper.Profiler
{
    public class MasterDetProfile : Profile
    {
        public MasterDetProfile()
        {
            #region GET
            CreateMap<MasterDet, MasterDetGetResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ReverseMap();
            #endregion

            #region CREATE
            CreateMap<MasterDetCreateRequestDto, MasterDet>()
           .ForMember(dest => dest.IdMaster, opt => opt.MapFrom(src => src.IdMaster))
           .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
           .ForMember(dest => dest.Value2, opt => opt.MapFrom(src => src.Value2))
           .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));

            CreateMap<MasterDet, MasterDetCreateResponseDto>();

            #endregion

            #region UPDATE
            CreateMap<MasterDetUpdateRequestDto, MasterDet>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<MasterDet, MasterDetUpdateResponseDto>();
            #endregion

            #region List
            CreateMap<MasterDet, MasterDetListResponseDto>();
            #endregion
        }
    }
}
