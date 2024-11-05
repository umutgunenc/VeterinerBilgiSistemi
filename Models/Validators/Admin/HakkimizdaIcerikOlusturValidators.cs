using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class HakkimizdaIcerikOlusturValidators :AbstractValidator<HakkimizdaIcerikOlusturViewModel>
    {
        public HakkimizdaIcerikOlusturValidators()
        {
            RuleFor(x => x.Baslik)
                .NotEmpty().WithMessage("Lütfen bir başlık giriniz.")
                .NotNull().WithMessage("Lütfen bir başlık giriniz.")
                .Must(FunctionsValidator.BeUniqueTitle).WithMessage("Girmiş olduğunuz başlık sisteme kayıtlı.");


            RuleFor(x=>x.Aciklama)
                .NotEmpty().WithMessage("Lütfen bir açıklama giriniz.")
                .NotNull().WithMessage("Lütfen bir açıklama giriniz.");
        }
    }
}
