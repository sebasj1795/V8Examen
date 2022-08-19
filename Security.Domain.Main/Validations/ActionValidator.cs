using FluentValidation;
using Security.Domain.Interfaces.IRepository;

namespace Security.Domain.MainModule.Validations
{
    public class ActionValidator : AbstractValidator<Domain.Entities.Action>
    {
        private readonly IActionRepository _actionRepository;
        public ActionValidator(IActionRepository actionRepository, int typeAction)
        {
            _actionRepository = actionRepository;
            RuleFor(x => x.Name).NotNull().Length(3, 50);
            RuleFor(x => x.State).NotNull();
            RuleFor(x => x.UserCrea).NotNull();
            RuleFor(x => x.DateCrea).NotNull();
        }
    }
}
