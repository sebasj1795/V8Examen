using AutoMapper.QueryableExtensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Security.Application.Dto.Action;
using Security.Application.Dto.Paginate;
using Security.Application.Interfaces;
using Security.Application.MainModule.Base;
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
    public class ActionAppService : BaseAppService, IActionAppService
    {
        private readonly IActionRepository _actionRepository;
        public ActionAppService(IServiceProvider serviceProvider,
            IActionRepository actionRepository) : base(serviceProvider)
        {
            _actionRepository = actionRepository;
        }

        public async Task<Response<ActionCreateResponseDto>> CreateAsync(ActionCreateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<ActionCreateResponseDto>();
            var actionEntity = Mapper.Map<Domain.Entities.Action>(request);
            actionEntity.State = (int)StateEnum.Active;
            actionEntity.UserCrea = CurrentUser.Id;
            actionEntity.DateCrea = DateTime.Now;
            var resultValidate = await _actionRepository.AddAsync(actionEntity, new ActionValidator(_actionRepository, (int)ActionCrudEnum.Create));

            if (resultValidate.IsValid)
            {
                await UnitOfWork.SaveChangesAsync();
                response.Data = Mapper.Map<ActionCreateResponseDto>(actionEntity);
                return response;
            }
            response = TypeMessageHelper.MessageWarning<ActionCreateResponseDto>(resultValidate.ToString("\n"));
            return response;
        }

        public async Task<Response<ActionUpdateResponseDto>> UpdateAsync(ActionUpdateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<ActionUpdateResponseDto>();

            var actionEntity = await _actionRepository.GetAsync(request.Id);

            if (actionEntity != null)
            {
                actionEntity.UserUpd = CurrentUser.Id;
                actionEntity.DateUpd = DateTime.Now;
                var resultValidate = await _actionRepository.UpdateAsync(actionEntity, new ActionValidator(_actionRepository, (int)ActionCrudEnum.Update));

                if (resultValidate.IsValid)
                {
                    await UnitOfWork.SaveChangesAsync();
                    response.Data = Mapper.Map<ActionUpdateResponseDto>(actionEntity);
                    return response;
                }
                response = TypeMessageHelper.MessageWarning<ActionUpdateResponseDto>(resultValidate.ToString("\n"));
            }
            return response;
        }

        public async Task<Response<ActionGetResponseDto>> GetByIdAsync(int id)
        {
            var response = TypeMessageHelper.MessageSuccess<ActionGetResponseDto>();
            var entity = await _actionRepository.GetAsync(id);
            var userDto = Mapper.Map<ActionGetResponseDto>(entity);
            response.Data = userDto;
            return response;
        }

        public async Task<Response<PaginateResponseDto<ActionListResponseDto>>> GetAllAsync(PaginateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<PaginateResponseDto<ActionListResponseDto>>();

            var predicate = PredicateBuilder.New<Domain.Entities.Action> (p => p.State != (int)StateEnum.Delete);

            var filterResult = await _actionRepository.FindAllPagingAsync(new PaginateRequest<Domain.Entities.Action>
            {
                ColumnOrder = request.ColumnOrder,
                Size = request.Size,
                Page = request.Page,
                Order = request.Order,
                WhereFilter = predicate
            });

            var listUser = await filterResult.List.ProjectTo<ActionListResponseDto>(Mapper.ConfigurationProvider).ToListAsync();

            PaginateResponseDto<ActionListResponseDto> paginateResponseDto = new PaginateResponseDto<ActionListResponseDto>();
            paginateResponseDto.Entities = listUser;
            paginateResponseDto.Count = listUser.Count();
            response.Data = paginateResponseDto;
            return response;
        }


    }
}
