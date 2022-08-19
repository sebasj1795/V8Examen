using AutoMapper.QueryableExtensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Security.Application.Dto.Company;
using Security.Application.Dto.Paginate;
using Security.Application.Interfaces;
using Security.Application.MainModule.Base;
using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Domain.MainModule.Validations;
using Security.Transversal.Common;
using Security.Transversal.Common.Enum;
using Security.Transversal.Common.Helpers;
using Security.Transversal.Common.Paginate;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Security.Application.MainModule
{
    public class CompanyAppService : BaseAppService, ICompanyAppService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyAppService(IServiceProvider serviceProvider,
            ICompanyRepository companyRepository) : base(serviceProvider)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Response<CompanyCreateResponseDto>> CreateAsync(CompanyCreateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<CompanyCreateResponseDto>();
            var companyEntity = Mapper.Map<Company>(request);
            companyEntity.State = (int)StateEnum.Active;
            companyEntity.UserCrea = CurrentUser.Id;
            companyEntity.DateCrea = DateTime.Now;
            var resultValidate = await _companyRepository.AddAsync(companyEntity, new CompanyValidator(_companyRepository, (int)ActionCrudEnum.Create));

            if (resultValidate.IsValid)
            {
                await UnitOfWork.SaveChangesAsync();
                response.Data = Mapper.Map<CompanyCreateResponseDto>(companyEntity);
                return response;
            }
            response = TypeMessageHelper.MessageWarning<CompanyCreateResponseDto>(resultValidate.ToString("\n"));
            return response;
        }

        public async Task<Response<CompanyUpdateResponseDto>> UpdateAsync(CompanyUpdateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<CompanyUpdateResponseDto>();

            var companyEntity = await _companyRepository.GetAsync(request.Id);

            if (companyEntity != null)
            {
                companyEntity.UserUpd = CurrentUser.Id;
                companyEntity.DateUpd = DateTime.Now;
                var resultValidate = await _companyRepository.UpdateAsync(companyEntity, new CompanyValidator(_companyRepository, (int)ActionCrudEnum.Update));

                if (resultValidate.IsValid)
                {
                    await UnitOfWork.SaveChangesAsync();
                    response.Data = Mapper.Map<CompanyUpdateResponseDto>(companyEntity);
                    return response;
                }
                response = TypeMessageHelper.MessageWarning<CompanyUpdateResponseDto>(resultValidate.ToString("\n"));
            }
            return response;
        }

        public async Task<Response<CompanyGetResponseDto>> GetByIdAsync(int id)
        {
            var response = TypeMessageHelper.MessageSuccess<CompanyGetResponseDto>();
            var entity = await _companyRepository.GetAsync(id);
            var userDto = Mapper.Map<CompanyGetResponseDto>(entity);
            response.Data = userDto;
            return response;
        }

        public async Task<Response<PaginateResponseDto<CompanyListResponseDto>>> GetAllAsync(PaginateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<PaginateResponseDto<CompanyListResponseDto>>();

            var predicate = PredicateBuilder.New<Company>(p => p.State != (int)StateEnum.Delete);

            var filterResult = await _companyRepository.FindAllPagingAsync(new PaginateRequest<Company>
            {
                ColumnOrder = request.ColumnOrder,
                Size = request.Size,
                Page = request.Page,
                Order = request.Order,
                WhereFilter = predicate
            });

            var listUser = await filterResult.List.ProjectTo<CompanyListResponseDto>(Mapper.ConfigurationProvider).ToListAsync();

            PaginateResponseDto<CompanyListResponseDto> paginateResponseDto = new PaginateResponseDto<CompanyListResponseDto>();
            paginateResponseDto.Entities = listUser;
            paginateResponseDto.Count = listUser.Count();
            response.Data = paginateResponseDto;
            return response;
        }

    }
}
