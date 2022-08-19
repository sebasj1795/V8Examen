using FluentValidation;
using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;

namespace Security.Domain.MainModule.Validations
{
    public class ModuleValidator : AbstractValidator<Module>
    {
        private readonly IModuleRepository _moduleRepository;
        public ModuleValidator(IModuleRepository moduleRepository, int type)
        {
            _moduleRepository = moduleRepository;

            RuleFor(x => x.IdApp).NotNull();
            RuleFor(x => x.Name).NotNull().Length(3, 50);
            RuleFor(x => x.Description).NotNull().Length(150);
            RuleFor(x => x.Order).NotNull();
            RuleFor(x => x.IconCss).Length(50);
            RuleFor(x => x.IconImg).Length(50);
            RuleFor(x => x.State).NotNull();
            RuleFor(x => x.UserCrea).NotNull();
            RuleFor(x => x.DateCrea).NotNull();

        }
    }
}
