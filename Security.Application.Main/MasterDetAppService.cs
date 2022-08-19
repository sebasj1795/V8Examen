using AutoMapper.QueryableExtensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Security.Application.Dto.Master;
using Security.Application.Dto.MasterDet;
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
    public class MasterDetAppService : BaseAppService, IMasterDetAppService
    {
        private readonly IMasterDetRepository _masterDetRepository;
        public MasterDetAppService(IServiceProvider serviceProvider, 
            IMasterDetRepository masterDetRepository) : base(serviceProvider)
        {
            _masterDetRepository = masterDetRepository;
        }

        public async Task<Response<MasterDetCreateResponseDto>> CreateAsync(MasterDetCreateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<MasterDetCreateResponseDto>();
            var masterDetEntity = Mapper.Map<MasterDet>(request);
            masterDetEntity.State = (int)StateEnum.Active;
            masterDetEntity.UserCrea = CurrentUser.Id;
            masterDetEntity.DateCrea = DateTime.Now;
            var resultValidate = await _masterDetRepository.AddAsync(masterDetEntity, new MasterDetValidator(_masterDetRepository, (int)ActionCrudEnum.Create));

            if (resultValidate.IsValid)
            {
                await UnitOfWork.SaveChangesAsync();
                response.Data = Mapper.Map<MasterDetCreateResponseDto>(masterDetEntity);
                return response;
            }
            response = TypeMessageHelper.MessageWarning<MasterDetCreateResponseDto>(resultValidate.ToString("\n"));
            return response;
        }

        public async Task<Response<MasterDetUpdateResponseDto>> UpdateAsync(MasterDetUpdateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<MasterDetUpdateResponseDto>();

            var userEntity = await _masterDetRepository.GetAsync(request.Id);

            if (userEntity != null)
            {
                userEntity.UserUpd = CurrentUser.Id;
                userEntity.DateUpd = DateTime.Now;
                var resultValidate = await _masterDetRepository.UpdateAsync(userEntity, new MasterDetValidator(_masterDetRepository, (int)ActionCrudEnum.Update));

                if (resultValidate.IsValid)
                {
                    await UnitOfWork.SaveChangesAsync();
                    response.Data = Mapper.Map<MasterDetUpdateResponseDto>(userEntity);
                    return response;
                }
                response = TypeMessageHelper.MessageWarning<MasterDetUpdateResponseDto>(resultValidate.ToString("\n"));
            }
            return response;
        }

        public async Task<Response<MasterDetGetResponseDto>> GetByIdAsync(int id)
        {
            var response = TypeMessageHelper.MessageSuccess<MasterDetGetResponseDto>();
            var entity = await _masterDetRepository.GetAsync(id);
            var userDto = Mapper.Map<MasterDetGetResponseDto>(entity);
            response.Data = userDto;
            return response;
        }

        public async Task<Response<PaginateResponseDto<MasterDetListResponseDto>>> GetAllAsync(PaginateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<PaginateResponseDto<MasterDetListResponseDto>>();

            var predicate = PredicateBuilder.New<MasterDet>(p => p.State != (int)StateEnum.Delete);

            var filterResult = await _masterDetRepository.FindAllPagingAsync(new PaginateRequest<MasterDet>
            {
                ColumnOrder = request.ColumnOrder,
                Size = request.Size,
                Page = request.Page,
                Order = request.Order,
                WhereFilter = predicate
            });

            var listUser = await filterResult.List.ProjectTo<MasterDetListResponseDto>(Mapper.ConfigurationProvider).ToListAsync();

            PaginateResponseDto<MasterDetListResponseDto> paginateResponseDto = new PaginateResponseDto<MasterDetListResponseDto>();
            paginateResponseDto.Entities = listUser;
            paginateResponseDto.Count = listUser.Count();
            response.Data = paginateResponseDto;
            return response;
        }

    }
}
