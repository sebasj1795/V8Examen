using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Security.Application.Dto.Login;
using Security.Application.Interfaces;
using Security.Application.MainModule.Base;
using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Transversal.Auth.Jwt;
using Security.Transversal.Common;
using Security.Transversal.Common.Enum;
using Security.Transversal.Common.Helpers;
using Security.Transversal.Common.Wapper;
using System;
using System.Threading.Tasks;

namespace Security.Application.MainModule
{
    public class LoginAppService : BaseAppService, ILoginAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppRepository _appRepository;
        private readonly IJwtFactory _jwtFactory;
        private readonly LoginLockSetting _loginLockSetting;
        public LoginAppService(IServiceProvider serviceProvider,
                               IUserRepository userRepository,
                               IAppRepository appRepository,
                               IJwtFactory jwtFactory,
                               IOptions<LoginLockSetting> loginLockConfig) : base(serviceProvider)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
            _loginLockSetting = loginLockConfig.Value;
            _appRepository = appRepository;
        }
        public async Task<Response<LoginResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess<LoginResponseDto>();

            var appDomain = await _appRepository.Find(x => x.Id == request.App && x.State == (int)StateEnum.Active).FirstAsync();

            var userDomain = await _userRepository
                .Find(p => p.UserName == request.Username && p.State == (int)StateEnum.Active, false)
                .Include(p => p.UserRoles)
                .FirstOrDefaultAsync();

            if (appDomain is null)
                return TypeMessageHelper.MessageWarning<LoginResponseDto>(Messages.Login.AppNotExist);

            if (userDomain is null)
                return TypeMessageHelper.MessageWarning<LoginResponseDto>(Messages.Login.UserNotExist);

            if (userDomain.NumberAttempt >= _loginLockSetting.AttemptsAllowed
                && userDomain.DateAttempt != null
                && DateTime.Now.Subtract(userDomain.DateAttempt.Value).TotalMinutes <= _loginLockSetting.LockTime)
            {
                return TypeMessageHelper.MessageWarning<LoginResponseDto>(Messages.Login.UserBlocked);
            }

            if (userDomain.ModeAuthentication == (byte)ModeAuthenticationEnum.DataBase)
            {
                if (!(!string.IsNullOrEmpty(request.Password) &&
                      userDomain.Password.Equals(EncriptHelper.Encriptar(request.Password))))
                {
                    var responseValidate = await ValidateFailedAttempt(userDomain);
                    if (responseValidate.Code != (int)CodeResponseEnum.Success) {
                        return TypeMessageHelper.Message<LoginResponseDto>(responseValidate);
                    }
                }

                //if (!userDomain.EmailConfirm.Value)
                //    return new LoginResponseDto { ConfirmedEmail = false };
            }
            else
            {
                //if (!_activeDirectoryAppService.Login(userDomain.UserName, request.Password))
                //{
                //    await ValidateFailedAttempt(userDomain);
                //}
            }

            userDomain.NumberAttempt = 0;
            var claimsJwt = Mapper.Map<Transversal.Auth.Entity.User>(userDomain);
            claimsJwt.AppId = appDomain.Id;
            claimsJwt.Platform = appDomain.Platform.ToString();
            claimsJwt.CompanyId = appDomain.IdCompany;
            var token = _jwtFactory.GetJwt(claimsJwt);
            response.Data = Mapper.Map<LoginResponseDto>(token);
            return response;
        }

        //private async Task RegisterUserLogin(Transversal.Auth.Entity.User user)
        //{
        //    await _logSessionUserRepository.AddAsync(new LogSessionUser
        //    {
        //        UserId = user.Id,
        //        Platform = user.Platform,
        //        LoginDate = DateTime.Now
        //    });
        //}

        private async Task<Response> ValidateFailedAttempt(User userDomain)
        {
            userDomain.NumberAttempt = userDomain.NumberAttempt >= _loginLockSetting.AttemptsAllowed
                ? 1
                : userDomain.NumberAttempt + 1;
            userDomain.DateAttempt = DateTime.Now;
            await UnitOfWork.SaveChangesAsync();

            if (_loginLockSetting.AttemptsAllowed - userDomain.NumberAttempt == 1)
            {
                return TypeMessageHelper.MessageWarning(Messages.Login.UserBlockedWarning);
            }
            else if (userDomain.NumberAttempt >= _loginLockSetting.AttemptsAllowed)
            {
                return TypeMessageHelper.MessageWarning(Messages.Login.UserBlocked);
            }
            return TypeMessageHelper.MessageWarning(Messages.Login.CredentialIncorrect);
        }

        public async Task<Response> ChangePasswordAsync(ChangePasswordRequestDto request)
        {
            var response = TypeMessageHelper.MessageSuccess();

            var userDomain = await _userRepository
                .Find(p => p.Id == CurrentUser.Id && p.State == (int)StateEnum.Active, false)
                .FirstOrDefaultAsync();

            if (userDomain is null)
                response = TypeMessageHelper.MessageWarning(Messages.Login.UserNotExist);

            if (userDomain.ModeAuthentication != (byte)ModeAuthenticationEnum.DataBase)
                response = TypeMessageHelper.MessageWarning(Messages.Login.ChangePasswordNotAllowed);

            if (!(!string.IsNullOrEmpty(request.NewPassword) &&
                  !string.IsNullOrEmpty(request.OldPassword) &&
                  userDomain.Password.Equals(EncriptHelper.Encriptar(request.OldPassword))))
                response = TypeMessageHelper.MessageWarning(Messages.Login.CredentialIncorrect);

            if (string.Equals(request.NewPassword, request.OldPassword, StringComparison.CurrentCultureIgnoreCase))
            response = TypeMessageHelper.MessageWarning(Messages.Login.CredentialsDifferent);

            userDomain.NumberAttempt = 0;
            userDomain.DateAttempt = null;
            userDomain.ChangePassword = true;
            userDomain.Password = EncriptHelper.Encriptar(request.NewPassword);
            await UnitOfWork.SaveChangesAsync();

            return response;
        }
    }
}
