using FluentValidation;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class HastalikTanimlaValidators : AbstractValidator<HastalikTanimlaViewModel>
    {
        public HastalikTanimlaValidators()
        {
            RuleFor(x => x.HastalikAdi)
                .NotNull().WithMessage("Hastalık adı boş olamaz.")
                .NotEmpty().WithMessage("Hastalık adı boş olamaz.")
                .MaximumLength(100).WithMessage("Hastalık adı en fazla 100 karakter uzunluğunda olabilir.")
                .Must(FunctionsValidator.BeUniqueHastalikAdi).WithMessage("Girilen hastalık zaten sisteme kayıtlı.");
        }
    }
}
