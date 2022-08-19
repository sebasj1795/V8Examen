using FluentValidation;
using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;

namespace Security.Domain.MainModule.Validations
{
    public class MasterDetValidator : AbstractValidator<MasterDet>
    {
        private readonly IMasterDetRepository _masterDetRepository;
        public MasterDetValidator(IMasterDetRepository masterDetRepository, int type)
        {

        }
    }
}
