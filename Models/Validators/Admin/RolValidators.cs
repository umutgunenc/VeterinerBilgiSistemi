using FluentValidation;
using System.Linq;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Entity;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public partial class RolValidators : AbstractValidator<RolEkleViewModel>
    {
        public RolValidators()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Lütfen rol tanımı yapınız.")
                .NotNull().WithMessage("Lütfen rol tanımı yapınız.")
                .MaximumLength(50).WithMessage("Maksimum 50 karakter uzunluğunda rol tanımlaması yapılabilir.")
                .Must(FunctionsValidator.BeUniqueRol).WithMessage("Girilen rol daha önceden sisteme tanımlanmış.")
                .Must(FunctionsValidator.BeAllowedRol).WithMessage("Sadece ADMIN, VETERINER, ÇALIŞAN ve MÜŞTERI tanımlanabilir.");
        }


    }
}
