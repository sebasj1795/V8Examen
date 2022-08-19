using AutoMapper.QueryableExtensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Security.Application.Dto.Paginate;
using Security.Application.Dto.User;
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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Security.Application.MainModule
{
    public class UserAppService : BaseAppService, IUserAppService
    {
        private readonly IUserRepository _userRepository;
        public UserAppService(IServiceProvider serviceProvider,
                              IUserRepository userRepository) : base(serviceProvider)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<UserCreateResponseDto>> CreateAsync(UserCreateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<UserCreateResponseDto>();
            var userEntity = Mapper.Map<User>(request);
            userEntity.NumberAttempt = 0;
            userEntity.State = (int)StateEnum.Active;
            userEntity.EmailConfirm = false;
            userEntity.UserCrea = CurrentUser.Id;
            userEntity.DateCrea = DateTime.Now;
            userEntity.ModeAuthentication = (int)ModeAuthenticationEnum.DataBase;
            userEntity.Password = EncriptHelper.Encriptar(request.Password);
            var resultValidate = await _userRepository.AddAsync(userEntity, new UserValidator(userEntity.Id, _userRepository, (int)ActionCrudEnum.Create));

            if (resultValidate.IsValid)
            {
                await UnitOfWork.SaveChangesAsync();
                response.Data = Mapper.Map<UserCreateResponseDto>(userEntity);
                return response;
            }
            response = TypeMessageHelper.MessageWarning<UserCreateResponseDto>(resultValidate.ToString("\n"));
            return response;
        }

        public async Task<Response<UserUpdateResponseDto>> UpdateAsync(UserUpdateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<UserUpdateResponseDto>();

            var userEntity = await _userRepository.GetAsync(request.Id);

            if (userEntity != null)
            {
                userEntity.Name = request.Name;
                userEntity.LastName = request.LastName;
                userEntity.UserName = request.UserName;
                userEntity.Password = request.Password;
                userEntity.Email = request.Email;
                userEntity.Expire = request.Expire;
                userEntity.DateExpire = request.DateExpire;
                userEntity.Password = EncriptHelper.Encriptar(request.Password);
                userEntity.SuperUser = request.SuperUser;
                userEntity.UserUpd = CurrentUser.Id;
                userEntity.DateUpd = DateTime.Now;
                var resultValidate = await _userRepository.UpdateAsync(userEntity, new UserValidator(userEntity.Id, _userRepository, (int)ActionCrudEnum.Update));

                if (resultValidate.IsValid)
                {
                    await UnitOfWork.SaveChangesAsync();
                    response.Data = Mapper.Map<UserUpdateResponseDto>(userEntity);
                    return response;
                }
                response = TypeMessageHelper.MessageWarning<UserUpdateResponseDto>(resultValidate.ToString("\n"));
            }
            return response;
        }

        public async Task<Response<UserGetResponseDto>> GetByIdAsync(int id)
        {
            var response = TypeMessageHelper.MessageSuccess<UserGetResponseDto>();
            var entity = await _userRepository.GetAsync(id);
            var userDto = Mapper.Map<UserGetResponseDto>(entity);
            response.Data = userDto;
            return response;
        }

        public async Task<Response<PaginateResponseDto<UserListResponseDto>>> GetAllAsync(PaginateRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<PaginateResponseDto<UserListResponseDto>>();

            var predicate = PredicateBuilder.New<User>(p => p.State != (int)StateEnum.Delete);

            var filterResult = await _userRepository.FindAllPagingAsync(new PaginateRequest<User>
            {
                ColumnOrder = request.ColumnOrder,
                Size = request.Size,
                Page = request.Page,
                Order = request.Order,
                WhereFilter = predicate
            });

            var listUser = await filterResult.List.ProjectTo<UserListResponseDto>(Mapper.ConfigurationProvider).ToListAsync();

            PaginateResponseDto<UserListResponseDto> paginateResponseDto = new PaginateResponseDto<UserListResponseDto>();
            paginateResponseDto.Entities = listUser;
            paginateResponseDto.Count = listUser.Count();
            response.Data = paginateResponseDto;
            return response;
        }

    }
}
