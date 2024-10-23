using FluentValidation;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class KanTahliliSilValidators :AbstractValidator<KanTahlilleriniGetirViewModel>
    {
        public KanTahliliSilValidators()
        {
            RuleFor(x => x.KanDegerleriId)
                .NotNull().WithMessage("Lütfen silinecek kan tahlilini seçiniz")
                .NotNull().WithMessage("Lütfen silinecek kan tahlilini seçiniz")
                .Must(FunctionsValidator.BeKanTahlili).WithMessage("Silinecek kan tahlili sistemde bulunamadı.")
                .Must(FunctionsValidator.BeNotUsedKanTahlili).WithMessage("Silinecek kan tahlili için sistemde kayıt bulunmaktadır. Silme işlemi başarısız oldu.");
        }
    }
}
