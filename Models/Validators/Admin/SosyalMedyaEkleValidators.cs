using FluentValidation;
using FluentValidation.Validators;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class SosyalMedyaEkleValidators :AbstractValidator<SosyalMedyaEkleViewModel>
    {
        public SosyalMedyaEkleValidators()
        {
            RuleFor(x => x.SosyalMedyaAdi)
                .NotEmpty().WithMessage("Lütfen sosyal medya adını giriniz.")
                .NotNull().WithMessage("Lütfen sosyal medya adını giriniz.")
                .MaximumLength(100).WithMessage("Sosyal medya adı maksimum 100 karakter olabilir.")
                .Must(FunctionsValidator.BeUniqueSosyalMedyaAdi).WithMessage("Girilen sosyal medya adı zaten sistemde kayıtlı.");

            RuleFor(x => x.SosyalMedyaPhotoUrl)
                .NotEmpty().WithMessage("Lütfen sosyal medya logosunun URL adresini giriniz.")
                .NotNull().WithMessage("Lütfen sosyal medya logosunun URL adresini giriniz.");

            RuleFor(x=>x.SosyalMedyaUrl)
                .NotEmpty().WithMessage("Lütfen sosyal medya için URL adresini giriniz.")
                .NotNull().WithMessage("Lütfen sosyal medya için URL adresini giriniz.");

        }
    }
}
