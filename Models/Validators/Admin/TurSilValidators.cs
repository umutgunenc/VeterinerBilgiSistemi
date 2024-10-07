using FluentValidation;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class TurSilValidator : AbstractValidator<TurSilViewModel>
    {
        public TurSilValidator()
        {

            RuleFor(x => x.TurId)
                .NotNull().WithMessage("Lütfen bir tür seçiniz.")
                .NotEmpty().WithMessage("Lütfen bir tür seçiniz.")
                .Must(FunctionsValidator.BeTur).WithMessage("Listede olmayan bir türü silemezsiniz.")
                .Must(FunctionsValidator.BeNotMatchedTur).WithMessage("Silinecek türe ait tanımlı cins olduğu için silme işlemi gerçekleştirilemedi.");
        }

    }
}