using FluentValidation;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class BirimSilValidators :AbstractValidator<BirimSilViewModel>
    {
        public BirimSilValidators()
        {
            RuleFor(x=>x.BirimId)
                .NotNull().WithMessage("Birim seçimi yapılmalıdır.")
                .NotEmpty().WithMessage("Birim seçimi yapılmalıdır.")
                .Must(FunctionsValidator.BeBirim).WithMessage("Seçilen Birim sistemde kayıtlı olmalıdır.")
                .Must(FunctionsValidator.BeNotUsedBirim).WithMessage("Seçilen Birim kullanıldığı için silme işlemi başarısız oldu.");
        }
    }
}
