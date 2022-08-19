using AutoMapper.QueryableExtensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Security.Application.Dto.Menu;
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
    public class MenuAppService : BaseAppService, IMenuAppService
    {
        private readonly IMenuRepository _menuRepository;
        public MenuAppService(IServiceProvider serviceProvider,
            IMenuRepository menuRepository) : base(serviceProvider)
        {
            _menuRepository = menuRepository;
        }

        public async Task<Response<MenuCreateResponseDto>> CreateAsync(MenuCreateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<MenuCreateResponseDto>();
            var roleEntity = Mapper.Map<Menu>(request);
            roleEntity.State = (int)StateEnum.Active;
            roleEntity.UserCrea = CurrentUser.Id;
            roleEntity.DateCrea = DateTime.Now;
            var resultValidate = await _menuRepository.AddAsync(roleEntity, new MenuValidator(_menuRepository, (int)ActionCrudEnum.Create));

            if (resultValidate.IsValid)
            {
                await UnitOfWork.SaveChangesAsync();
                response.Data = Mapper.Map<MenuCreateResponseDto>(roleEntity);
                return response;
            }
            response = TypeMessageHelper.MessageWarning<MenuCreateResponseDto>(resultValidate.ToString("\n"));
            return response;
        }

        public async Task<Response<MenuUpdateResponseDto>> UpdateAsync(MenuUpdateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<MenuUpdateResponseDto>();

            var userEntity = await _menuRepository.GetAsync(request.Id);

            if (userEntity != null)
            {
                userEntity.UserUpd = CurrentUser.Id;
                userEntity.DateUpd = DateTime.Now;
                var resultValidate = await _menuRepository.UpdateAsync(userEntity, new MenuValidator(_menuRepository, (int)ActionCrudEnum.Update));

                if (resultValidate.IsValid)
                {
                    await UnitOfWork.SaveChangesAsync();
                    response.Data = Mapper.Map<MenuUpdateResponseDto>(userEntity);
                    return response;
                }
                response = TypeMessageHelper.MessageWarning<MenuUpdateResponseDto>(resultValidate.ToString("\n"));
            }
            return response;
        }

        public async Task<Response<MenuGetResponseDto>> GetByIdAsync(int id)
        {
            var response = TypeMessageHelper.MessageSuccess<MenuGetResponseDto>();
            var entity = await _menuRepository.GetAsync(id);
            var userDto = Mapper.Map<MenuGetResponseDto>(entity);
            response.Data = userDto;
            return response;
        }

        public async Task<Response<PaginateResponseDto<MenuListResponseDto>>> GetAllAsync(PaginateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<PaginateResponseDto<MenuListResponseDto>>();

            var predicate = PredicateBuilder.New<Menu>(p => p.State != (int)StateEnum.Delete);

            var filterResult = await _menuRepository.FindAllPagingAsync(new PaginateRequest<Menu>
            {
                ColumnOrder = request.ColumnOrder,
                Size = request.Size,
                Page = request.Page,
                Order = request.Order,
                WhereFilter = predicate
            });

            var listUser = await filterResult.List.ProjectTo<MenuListResponseDto>(Mapper.ConfigurationProvider).ToListAsync();

            PaginateResponseDto<MenuListResponseDto> paginateResponseDto = new PaginateResponseDto<MenuListResponseDto>();
            paginateResponseDto.Entities = listUser;
            paginateResponseDto.Count = listUser.Count();
            response.Data = paginateResponseDto;
            return response;
        }


    }
}
