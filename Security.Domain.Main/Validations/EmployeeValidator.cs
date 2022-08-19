using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Transversal.Common.Enum;
using System.Threading.Tasks;
using System.Linq;
namespace Security.Domain.MainModule.Validations
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeValidator(IEmployeeRepository employeeRepository, int typeAction)
        {
            _employeeRepository = employeeRepository;
            RuleFor(x => x).MustAsync((user, cancel) => { return DuplicateName(user, typeAction); })
                .WithMessage("El nombre de empleado ya existe");
            RuleFor(x => x).MustAsync((user, cancel) => { return DuplicateIdentification(user); })
                .WithMessage("El código de identificación ya está asignado a un empleado");
            RuleFor(x => x.Year).NotNull().NotEmpty();
            RuleFor(x => x.Month).NotNull().NotEmpty();
            RuleFor(x => x.Office).NotNull().NotEmpty();
            RuleFor(x => x.EmployeeCode).NotNull().NotEmpty();
            RuleFor(x => x.EmployeeName).NotNull().NotEmpty().Length(3, 100);
            RuleFor(x => x.EmployeeSurname).NotNull().NotEmpty().Length(3, 100);
            RuleFor(x => x.Division).NotNull().NotEmpty();
            RuleFor(x => x.Position).NotNull().NotEmpty();
            RuleFor(x => x.Grade).NotNull().NotEmpty();
            RuleFor(x => x.BeginDate).NotNull().NotEmpty();
            RuleFor(x => x.Birthday).NotNull().NotEmpty();
            RuleFor(x => x.IdentificationNumber).NotNull().NotEmpty();
            RuleFor(x => x.BaseSalary).NotNull();
            RuleFor(x => x.ProductionBonus).NotNull();
            RuleFor(x => x.CompesationBonus).NotNull();
            RuleFor(x => x.Comission).NotNull();
            RuleFor(x => x.Constributions).NotNull();
        }

        public async Task<bool> DuplicateName(Employee employee, int type)
        {
            bool isDuplicate = false;
            if (type == (int)ActionCrudEnum.Create)
            {
                string nameComplete = string.Concat(employee.EmployeeName, employee.EmployeeSurname);
                isDuplicate = await _employeeRepository.Find(x => string.Concat(x.EmployeeName,x.EmployeeSurname).Trim() == nameComplete.Trim()).AnyAsync();
            }
            return !isDuplicate;
        }

        public async Task<bool> DuplicateIdentification(Employee employee)
        {
            bool isDuplicate = false;
            string codeActual = _employeeRepository.Find(x => x.Id == employee.Id).Select(x => x.EmployeeCode).FirstOrDefault();
            if (codeActual != null) 
                 isDuplicate = await _employeeRepository.Find(x => x.IdentificationNumber.Trim() == employee.IdentificationNumber.Trim() && x.EmployeeCode!= codeActual).AnyAsync();
            else
                isDuplicate = await _employeeRepository.Find(x => x.IdentificationNumber.Trim() == employee.IdentificationNumber.Trim()).AnyAsync();
            return !isDuplicate;
        }

    }
}
