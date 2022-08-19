using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Transversal.Common.Enum;
using System.Threading.Tasks;

namespace Security.Domain.MainModule.Validations
{
    public class MasterValidator : AbstractValidator<Master>
    {
        private readonly IMasterRepository _masterRepository;
        public MasterValidator(IMasterRepository masterRepository, int type)
        {
            _masterRepository = masterRepository;

            RuleFor(x => x).MustAsync((master, cancel) => { return DuplicateNameMaster(master, type); })
                .WithMessage("El nombre ya existe");
            RuleFor(x => x.IdCompany).NotNull();
            RuleFor(x => x.Name).NotNull().Length(2,100);
            RuleFor(x => x.State).NotNull();
            RuleFor(x => x.UserCrea).NotNull();
            RuleFor(x => x.DateCrea).NotNull();
        }

        private async Task<bool> DuplicateNameMaster(Master master, int type)
        {
            bool isDuplicate = false;
            if (type == (int)ActionCrudEnum.Create || type == (int)ActionCrudEnum.Update)
                isDuplicate = await _masterRepository.Find(x => x.Name == master.Name).AnyAsync();
            return isDuplicate;
        }

    }
}
