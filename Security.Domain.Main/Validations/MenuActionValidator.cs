using FluentValidation;
using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;

namespace Security.Domain.MainModule.Validations
{
    public class MenuActionValidator : AbstractValidator<MenuAction>
    {
        private readonly IMenuActionRepository _menuActionRepository;
        public MenuActionValidator(IMenuActionRepository menuActionRepository, int type)
        {
            _menuActionRepository = menuActionRepository;

            RuleFor(x => x.IdMenu).NotNull();
            RuleFor(x => x.IdAction).NotNull();
            RuleFor(x => x.State).NotNull();
            RuleFor(x => x.UserCrea).NotNull();
            RuleFor(x => x.DateCrea).NotNull();
        }
    }
}
