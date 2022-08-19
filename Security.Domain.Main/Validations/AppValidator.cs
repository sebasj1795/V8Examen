using FluentValidation;
using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;

namespace Security.Domain.MainModule.Validations
{
    public class AppValidator : AbstractValidator<App>
    {
        //private readonly IAppRepository _appRepository;
        public AppValidator(IAppRepository appRepository, int type)
        {
            //_appRepository = appRepository;

            RuleFor(x => x.Name).NotNull().NotEmpty().Length(3, 70);
            RuleFor(x => x.Server).NotNull().NotEmpty().Length(3, 50);
            RuleFor(x => x.UserServer).NotNull().NotEmpty().Length(100);
            RuleFor(x => x.PasswordServer).NotNull().NotEmpty().Length(150);
            RuleFor(x => x.Port).Length(4);
            RuleFor(x => x.NameBd).NotNull().NotEmpty().Length(25);
            RuleFor(x => x.IdCompany).NotNull();
            RuleFor(x => x.State).NotNull();
            RuleFor(x => x.UserCrea).NotNull();
            RuleFor(x => x.DateCrea).NotNull();

        }
    }
}
