using AutoMapper.QueryableExtensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Security.Application.Dto.App;
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
    public class AppAppService : BaseAppService, IAppAppService
    {
        private readonly IAppRepository _appRepository;
        public AppAppService(IServiceProvider serviceProvider,
            IAppRepository appRepository) : base(serviceProvider)
        {
            _appRepository = appRepository;
        }

        public async Task<Response<AppCreateResponseDto>> CreateAsync(AppCreateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<AppCreateResponseDto>();
            var roleEntity = Mapper.Map<App>(request);
            roleEntity.State = (int)StateEnum.Active;
            roleEntity.UserCrea = CurrentUser.Id;
            roleEntity.DateCrea = DateTime.Now;
            var resultValidate = await _appRepository.AddAsync(roleEntity, new AppValidator(_appRepository, (int)ActionCrudEnum.Create));

            if (resultValidate.IsValid)
            {
                await UnitOfWork.SaveChangesAsync();
                response.Data = Mapper.Map<AppCreateResponseDto>(roleEntity);
                return response;
            }
            response = TypeMessageHelper.MessageWarning<AppCreateResponseDto>(resultValidate.ToString("\n"));
            return response;
        }

        public async Task<Response<AppUpdateResponseDto>> UpdateAsync(AppUpdateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<AppUpdateResponseDto>();

            var userEntity = await _appRepository.GetAsync(request.Id);

            if (userEntity != null)
            {
                userEntity.UserUpd = CurrentUser.Id;
                userEntity.DateUpd = DateTime.Now;
                var resultValidate = await _appRepository.UpdateAsync(userEntity, new AppValidator(_appRepository, (int)ActionCrudEnum.Update));

                if (resultValidate.IsValid)
                {
                    await UnitOfWork.SaveChangesAsync();
                    response.Data = Mapper.Map<AppUpdateResponseDto>(userEntity);
                    return response;
                }
                response = TypeMessageHelper.MessageWarning<AppUpdateResponseDto>(resultValidate.ToString("\n"));
            }
            return response;
        }

        public async Task<Response<AppGetResponseDto>> GetByIdAsync(int id)
        {
            var response = TypeMessageHelper.MessageSuccess<AppGetResponseDto>();
            var entity = await _appRepository.GetAsync(id);
            var userDto = Mapper.Map<AppGetResponseDto>(entity);
            response.Data = userDto;
            return response;
        }

        public async Task<Response<PaginateResponseDto<AppListResponseDto>>> GetAllAsync(PaginateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<PaginateResponseDto<AppListResponseDto>>();

            var predicate = PredicateBuilder.New<App>(p => p.State != (int)StateEnum.Delete);

            var filterResult = await _appRepository.FindAllPagingAsync(new PaginateRequest<App>
            {
                ColumnOrder = request.ColumnOrder,
                Size = request.Size,
                Page = request.Page,
                Order = request.Order,
                WhereFilter = predicate
            });

            var listUser = await filterResult.List.ProjectTo<AppListResponseDto>(Mapper.ConfigurationProvider).ToListAsync();

            PaginateResponseDto<AppListResponseDto> paginateResponseDto = new PaginateResponseDto<AppListResponseDto>();
            paginateResponseDto.Entities = listUser;
            paginateResponseDto.Count = listUser.Count();
            response.Data = paginateResponseDto;
            return response;
        }


    }
}
