using FluentValidation;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class HastalikSilValidators : AbstractValidator<HastalikSilViewModel>
    {
        //TODO hastalik silme işlemi için kontrol ekle
        public HastalikSilValidators()
        {
            RuleFor(x => x.HastalikId)
                .NotEmpty().WithMessage("Lütfen bir hastalık seçiniz.")
                .NotNull().WithMessage("Lütfen bir hastalık seçiniz")
                .Must(FunctionsValidator.BeHastalik).WithMessage("Seçilen hastalık sisteme kayıtlı değil.");
                //.Must(FunctionsValidator.BeNotUsedHastalik).WithMessage("Bazı hayvanlar için bu hastalık tanımlandığı için silme işlemi başarısız oldu.");
        }
    }
}
