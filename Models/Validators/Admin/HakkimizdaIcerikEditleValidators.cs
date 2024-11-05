using FluentValidation;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class HakkimizdaIcerikEditleValidators : AbstractValidator<HakkimizdaIcerikEditleViewModel>
    {
        public HakkimizdaIcerikEditleValidators()
        {
            RuleFor(x => x.Baslik)
                .NotEmpty().WithMessage("Lütfen bir başlık giriniz.")
                .NotNull().WithMessage("Lütfen bir başlık giriniz.")
                .Must((model, Baslik) =>FunctionsValidator.BeUniqueTitle(model.HakkimizdaId, Baslik)).WithMessage("Girmiş olduğunuz başlık sisteme kayıtlı.");


            RuleFor(x => x.Aciklama)
                .NotEmpty().WithMessage("Lütfen bir açıklama giriniz.")
                .NotNull().WithMessage("Lütfen bir açıklama giriniz.");
        }
    }
}
