using AutoMapper;
using Security.Application.Dto.Company;
using Security.Domain.Entities;

namespace Security.Transversal.Mapper.Profiler
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            #region GET
            CreateMap<Company, CompanyGetResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Ruc, opt => opt.MapFrom(src => src.Ruc))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ReverseMap();
            #endregion

            #region Create
            CreateMap<CompanyCreateRequestDto, Company>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Ruc, opt => opt.MapFrom(src => src.Ruc))
               .ReverseMap();

            CreateMap<Company, CompanyCreateResponseDto>()
                .ReverseMap();
            #endregion

            #region Update
            CreateMap<CompanyUpdateRequestDto, Company>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<Company, CompanyUpdateResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
            #endregion

        }
    }
}
