using AutoMapper.QueryableExtensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Security.Application.Dto.Paginate;
using Security.Application.Dto.Role;
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
    public class RoleAppService : BaseAppService, IRoleAppService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleAppService(IServiceProvider serviceProvider,
            IRoleRepository roleRepository) : base(serviceProvider)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Response<RoleCreateResponseDto>> CreateAsync(RoleCreateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<RoleCreateResponseDto>();
            var roleEntity = Mapper.Map<Role>(request);
            roleEntity.State = (int)StateEnum.Active;
            roleEntity.UserCrea = CurrentUser.Id;
            roleEntity.DateCrea = DateTime.Now;
            var resultValidate = await _roleRepository.AddAsync(roleEntity, new RoleValidator(_roleRepository, (int)ActionCrudEnum.Create));

            if (resultValidate.IsValid)
            {
                await UnitOfWork.SaveChangesAsync();
                response.Data = Mapper.Map<RoleCreateResponseDto>(roleEntity);
                return response;
            }
            response = TypeMessageHelper.MessageWarning<RoleCreateResponseDto>(resultValidate.ToString("\n"));
            return response;
        }

        public async Task<Response<RoleUpdateResponseDto>> UpdateAsync(RoleUpdateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<RoleUpdateResponseDto>();

            var userEntity = await _roleRepository.GetAsync(request.Id);

            if (userEntity != null)
            {
                userEntity.UserUpd = CurrentUser.Id;
                userEntity.DateUpd = DateTime.Now;
                var resultValidate = await _roleRepository.UpdateAsync(userEntity, new RoleValidator(_roleRepository, (int)ActionCrudEnum.Update));

                if (resultValidate.IsValid)
                {
                    await UnitOfWork.SaveChangesAsync();
                    response.Data = Mapper.Map<RoleUpdateResponseDto>(userEntity);
                    return response;
                }
                response = TypeMessageHelper.MessageWarning<RoleUpdateResponseDto>(resultValidate.ToString("\n"));
            }
            return response;
        }

        public async Task<Response<RoleGetResponseDto>> GetByIdAsync(int id)
        {
            var response = TypeMessageHelper.MessageSuccess<RoleGetResponseDto>();
            var entity = await _roleRepository.GetAsync(id);
            var userDto = Mapper.Map<RoleGetResponseDto>(entity);
            response.Data = userDto;
            return response;
        }

        public async Task<Response<PaginateResponseDto<RoleListResponseDto>>> GetAllAsync(PaginateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<PaginateResponseDto<RoleListResponseDto>>();

            var predicate = PredicateBuilder.New<Role>(p => p.State != (int)StateEnum.Delete);

            var filterResult = await _roleRepository.FindAllPagingAsync(new PaginateRequest<Role>
            {
                ColumnOrder = request.ColumnOrder,
                Size = request.Size,
                Page = request.Page,
                Order = request.Order,
                WhereFilter = predicate
            });

            var listUser = await filterResult.List.ProjectTo<RoleListResponseDto>(Mapper.ConfigurationProvider).ToListAsync();

            PaginateResponseDto<RoleListResponseDto> paginateResponseDto = new PaginateResponseDto<RoleListResponseDto>();
            paginateResponseDto.Entities = listUser;
            paginateResponseDto.Count = listUser.Count();
            response.Data = paginateResponseDto;
            return response;
        }


    }
}
