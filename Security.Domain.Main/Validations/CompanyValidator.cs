using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Transversal.Common.Enum;
using System.Threading.Tasks;

namespace Security.Domain.MainModule.Validations
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyValidator(ICompanyRepository companyRepository, int type)
        {
            _companyRepository = companyRepository;
            RuleFor(x => x).MustAsync((user, cancel) => { return DuplicateName(user, type); })
                .WithMessage("El nombre ya existe");
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(3, 50);
            RuleFor(x => x.Ruc).NotNull().NotEmpty().Length(11);
            RuleFor(x => x.State).NotNull();
            RuleFor(x => x.UserCrea).NotNull();
            RuleFor(x => x.DateCrea).NotNull();
            RuleFor(x => x.UserUpd).NotNull();
            RuleFor(x => x.DateUpd).NotNull();

        }

        public async Task<bool> DuplicateName(Company company, int type)
        {
            bool isDuplicate = false;
            if (type == (int)ActionCrudEnum.Create)
                isDuplicate = await _companyRepository.Find(x => x.Name == company.Name).AnyAsync();
            return isDuplicate;
        }

    }
}
