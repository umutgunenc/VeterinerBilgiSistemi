using FluentValidation;
using System.Linq;
using VeterinerBilgiSistemi.Data;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

#nullable disable

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public partial class TurEkleValidators : AbstractValidator<TurEKleViewModel>
    {

        public TurEkleValidators()
        {

            RuleFor(x => x.TurAdi)
                .NotEmpty().WithMessage("Lütfen bir tür giriniz")
                .NotNull().WithMessage("Lütfen bir tür giriniz")
                .MaximumLength(50).WithMessage("En fazla 50 karakter uzunluğunda değer girilebilir.")
                .Must(FunctionsValidator.BeUniqueTur).WithMessage("Girdiğiniz tür zaten sistemde kayıtlı.");
        }


    }
}
