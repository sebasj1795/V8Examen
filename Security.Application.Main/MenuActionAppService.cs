using AutoMapper.QueryableExtensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Security.Application.Dto.MenuAction;
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
    public class MenuActionAppService : BaseAppService, IMenuActionAppService
    {
        private readonly IMenuActionRepository _menuActionRepository;
        public MenuActionAppService(IServiceProvider serviceProvider,
            IMenuActionRepository menuActionRepository) : base(serviceProvider)
        {
            _menuActionRepository = menuActionRepository;
        }

        public async Task<Response<MenuActionCreateResponseDto>> CreateAsync(MenuActionCreateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<MenuActionCreateResponseDto>();
            var roleEntity = Mapper.Map<MenuAction>(request);
            roleEntity.State = (int)StateEnum.Active;
            roleEntity.UserCrea = CurrentUser.Id;
            roleEntity.DateCrea = DateTime.Now;
            var resultValidate = await _menuActionRepository.AddAsync(roleEntity, new MenuActionValidator(_menuActionRepository, (int)ActionCrudEnum.Create));

            if (resultValidate.IsValid)
            {
                await UnitOfWork.SaveChangesAsync();
                response.Data = Mapper.Map<MenuActionCreateResponseDto>(roleEntity);
                return response;
            }
            response = TypeMessageHelper.MessageWarning<MenuActionCreateResponseDto>(resultValidate.ToString("\n"));
            return response;
        }

        public async Task<Response<MenuActionUpdateResponseDto>> UpdateAsync(MenuActionUpdateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<MenuActionUpdateResponseDto>();

            var userEntity = await _menuActionRepository.GetAsync(request.Id);

            if (userEntity != null)
            {
                userEntity.UserUpd = CurrentUser.Id;
                userEntity.DateUpd = DateTime.Now;
                var resultValidate = await _menuActionRepository.UpdateAsync(userEntity, new MenuActionValidator(_menuActionRepository, (int)ActionCrudEnum.Update));

                if (resultValidate.IsValid)
                {
                    await UnitOfWork.SaveChangesAsync();
                    response.Data = Mapper.Map<MenuActionUpdateResponseDto>(userEntity);
                    return response;
                }
                response = TypeMessageHelper.MessageWarning<MenuActionUpdateResponseDto>(resultValidate.ToString("\n"));
            }
            return response;
        }

        public async Task<Response<MenuActionGetResponseDto>> GetByIdAsync(int id)
        {
            var response = TypeMessageHelper.MessageSuccess<MenuActionGetResponseDto>();
            var entity = await _menuActionRepository.GetAsync(id);
            var userDto = Mapper.Map<MenuActionGetResponseDto>(entity);
            response.Data = userDto;
            return response;
        }

        public async Task<Response<PaginateResponseDto<MenuActionListResponseDto>>> GetAllAsync(PaginateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<PaginateResponseDto<MenuActionListResponseDto>>();

            var predicate = PredicateBuilder.New<MenuAction>(p => p.State != (int)StateEnum.Delete);

            var filterResult = await _menuActionRepository.FindAllPagingAsync(new PaginateRequest<MenuAction>
            {
                ColumnOrder = request.ColumnOrder,
                Size = request.Size,
                Page = request.Page,
                Order = request.Order,
                WhereFilter = predicate
            });

            var listUser = await filterResult.List.ProjectTo<MenuActionListResponseDto>(Mapper.ConfigurationProvider).ToListAsync();

            PaginateResponseDto<MenuActionListResponseDto> paginateResponseDto = new PaginateResponseDto<MenuActionListResponseDto>();
            paginateResponseDto.Entities = listUser;
            paginateResponseDto.Count = listUser.Count();
            response.Data = paginateResponseDto;
            return response;
        }
    }
}
