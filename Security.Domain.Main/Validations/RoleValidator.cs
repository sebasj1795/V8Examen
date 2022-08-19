using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Transversal.Common.Enum;
using System.Threading.Tasks;

namespace Security.Domain.MainModule.Validations
{
    public class RoleValidator : AbstractValidator<Role>
    {
        private readonly IRoleRepository _roleRepository;
        public RoleValidator(IRoleRepository roleRepository, int type)
        {
            _roleRepository = roleRepository;

            RuleFor(x => x).MustAsync((user, cancel) => { return DuplicateName(user, type); })
                .WithMessage("El nombre ya existe");
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(3, 50);
            RuleFor(x => x.Comment).Length(150);
            RuleFor(x => x.State).NotNull();
            RuleFor(x => x.UserCrea).NotNull();
            RuleFor(x => x.DateCrea).NotNull();
            RuleFor(x => x.UserUpd).NotNull();
            RuleFor(x => x.DateUpd).NotNull();
        }

        public async Task<bool> DuplicateName(Role role, int type)
        {
            bool isDuplicate = false;
            if (type == (int)ActionCrudEnum.Create)
                isDuplicate = await _roleRepository.Find(x => x.Name == role.Name).AnyAsync();
            return isDuplicate;
        }

    }
}
