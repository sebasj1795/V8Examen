using AutoMapper.QueryableExtensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Security.Application.Dto.Module;
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
    public class ModuleAppService : BaseAppService, IModuleAppService
    {
        private readonly IModuleRepository _moduleRepository;
        public ModuleAppService(IServiceProvider serviceProvider,
            IModuleRepository moduleRepository) : base(serviceProvider)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task<Response<ModuleCreateResponseDto>> CreateAsync(ModuleCreateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<ModuleCreateResponseDto>();
            var roleEntity = Mapper.Map<Module>(request);
            roleEntity.State = (int)StateEnum.Active;
            roleEntity.UserCrea = CurrentUser.Id;
            roleEntity.DateCrea = DateTime.Now;
            var resultValidate = await _moduleRepository.AddAsync(roleEntity, new ModuleValidator(_moduleRepository, (int)ActionCrudEnum.Create));

            if (resultValidate.IsValid)
            {
                await UnitOfWork.SaveChangesAsync();
                response.Data = Mapper.Map<ModuleCreateResponseDto>(roleEntity);
                return response;
            }
            response = TypeMessageHelper.MessageWarning<ModuleCreateResponseDto>(resultValidate.ToString("\n"));
            return response;
        }

        public async Task<Response<ModuleUpdateResponseDto>> UpdateAsync(ModuleUpdateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<ModuleUpdateResponseDto>();

            var userEntity = await _moduleRepository.GetAsync(request.Id);

            if (userEntity != null)
            {
                userEntity.UserUpd = CurrentUser.Id;
                userEntity.DateUpd = DateTime.Now;
                var resultValidate = await _moduleRepository.UpdateAsync(userEntity, new ModuleValidator(_moduleRepository, (int)ActionCrudEnum.Update));

                if (resultValidate.IsValid)
                {
                    await UnitOfWork.SaveChangesAsync();
                    response.Data = Mapper.Map<ModuleUpdateResponseDto>(userEntity);
                    return response;
                }
                response = TypeMessageHelper.MessageWarning<ModuleUpdateResponseDto>(resultValidate.ToString("\n"));
            }
            return response;
        }

        public async Task<Response<ModuleGetResponseDto>> GetByIdAsync(int id)
        {
            var response = TypeMessageHelper.MessageSuccess<ModuleGetResponseDto>();
            var entity = await _moduleRepository.GetAsync(id);
            var userDto = Mapper.Map<ModuleGetResponseDto>(entity);
            response.Data = userDto;
            return response;
        }

        public async Task<Response<PaginateResponseDto<ModuleListResponseDto>>> GetAllAsync(PaginateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<PaginateResponseDto<ModuleListResponseDto>>();

            var predicate = PredicateBuilder.New<Module>(p => p.State != (int)StateEnum.Delete);

            var filterResult = await _moduleRepository.FindAllPagingAsync(new PaginateRequest<Module>
            {
                ColumnOrder = request.ColumnOrder,
                Size = request.Size,
                Page = request.Page,
                Order = request.Order,
                WhereFilter = predicate
            });

            var listUser = await filterResult.List.ProjectTo<ModuleListResponseDto>(Mapper.ConfigurationProvider).ToListAsync();

            PaginateResponseDto<ModuleListResponseDto> paginateResponseDto = new PaginateResponseDto<ModuleListResponseDto>();
            paginateResponseDto.Entities = listUser;
            paginateResponseDto.Count = listUser.Count();
            response.Data = paginateResponseDto;
            return response;
        }

    }
}
