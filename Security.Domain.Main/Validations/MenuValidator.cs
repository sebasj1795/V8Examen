

using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Transversal.Common.Enum;
using System.Threading.Tasks;

namespace Security.Domain.MainModule.Validations
{
    public class MenuValidator : AbstractValidator<Menu>
    {
        private readonly IMenuRepository _menuRepository;
        public MenuValidator(IMenuRepository menuRepository, int type)
        {
            _menuRepository = menuRepository;

            RuleFor(x => x.IdModule).NotNull();
            RuleFor(x => x.Name).NotNull().Length(3, 50);
            RuleFor(x => x.Url).Length(3, 100);
            RuleFor(x => x.IconCss).Length(50);
            RuleFor(x => x.IconImg).Length(50);
            RuleFor(x => x.State).NotNull();
            RuleFor(x => x.UserCrea).NotNull();
            RuleFor(x => x.DateCrea).NotNull();

        }

        public async Task<bool> DuplicateName(Menu role, int type)
        {
            bool isDuplicate = false;
            if (type == (int)ActionCrudEnum.Create)
                isDuplicate = await _menuRepository.Find(x => x.Name == role.Name).AnyAsync();
            return isDuplicate;
        }

    }
}
