using FluentValidation;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class SecilenKanTahlilDetayiValidators :AbstractValidator<KanTahlilleriniGetirViewModel>
    {
        public SecilenKanTahlilDetayiValidators()
        {
            RuleFor(x => x.KanDegerleriId)
                .NotEmpty().WithMessage("Lütfen Bir Kan Tahlili Seçiniz.")
                .NotNull().WithMessage("Lütfen Bir Kan Tahlili Seçiniz.")
                .Must(FunctionsValidator.BeKanTahlili).WithMessage("Seçilen Kan Tahlili Sistemde Bulunamadı.");
        }
    }
}
