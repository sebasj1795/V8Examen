using FluentValidation;
using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Security.Transversal.Common.Enum;
using System.Linq;

namespace Security.Domain.MainModule.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        private readonly IUserRepository _userRepository;
        public UserValidator(int currentUserId, IUserRepository userRepository, int typeAction)
        {
            _userRepository = userRepository;

            RuleFor(x => x).MustAsync((user, cancel) => { return DuplicateUsername(currentUserId, user, typeAction); }).WithMessage("El usuario ingresado ya existe");
            RuleFor(x => x).MustAsync((user, cancel) => { return Exist(user, typeAction); }).WithMessage("Usuario no existe");
            RuleFor(x => x.LastName).NotNull().Length(3, 50);
            RuleFor(x => x.UserName).NotNull().NotEmpty().Length(3, 50);
            RuleFor(x => x.Password).NotNull().NotEmpty().Length(6, 150);
            RuleFor(x => x.Email).Length(3, 50).EmailAddress();
            RuleFor(x => x.State).NotNull();
            RuleFor(x => x.Expire).NotNull();
            RuleFor(x => x.SuperUser).NotNull();
            //RuleFor(x => x).MustAsync((user, cancel) => { return ValidateRoleId(user); }).WithMessage("Seleccione un rol");
            RuleFor(x => x.EmailConfirm).NotNull();
            RuleFor(x => x.ModeAuthentication).NotNull();
        }

        private async Task<bool> DuplicateUsername(int CurrentUserId, User user, int type)
        {
            bool IsValid = true;
            if (type == (int)ActionCrudEnum.Create)
            {
                bool isDuplicate = await _userRepository.Find(x => x.UserName == user.UserName && x.State == (int)StateEnum.Delete).AnyAsync();
                IsValid = isDuplicate ? false : isDuplicate;
            }
            else if (type == (int)ActionCrudEnum.Update)
            {
                var UserSesion = await _userRepository.GetAsync(CurrentUserId);
                bool isDuplicate = await _userRepository.Find(x => x.UserName == user.UserName && x.UserName != UserSesion.UserName && x.State == (int)StateEnum.Delete).AnyAsync();
                IsValid = !isDuplicate ? true : isDuplicate;
            }

            return IsValid;
        }

        private async Task<bool> Exist(User user, int type)
        {
            bool IsValid = true;
            if (type != (int)ActionCrudEnum.Create)
            {
                IsValid = await _userRepository.Find(x => x.Id == user.Id && x.State != (int)StateEnum.Delete).AnyAsync();
            }
            return IsValid;
        }

        //private async Task<bool> ValidateRoleId(User user)
        //{
        //    bool IsValid = (user.IdRol != 0);
        //    return IsValid;
        //}
    }
}
