using FluentValidation;
using VeterinerBilgiSistemi.Models.Validators.ValidateFunctions;
using VeterinerBilgiSistemi.Models.ViewModel.Admin;

namespace VeterinerBilgiSistemi.Models.Validators.Admin
{
    public class AdresEkleValidators : AbstractValidator<AdresEkleViewModel>
    {
        public AdresEkleValidators()
        {
            RuleFor(x => x.SubeAdi)
                .NotEmpty().WithMessage("Lütfen şube adını giriniz.")
                .NotNull().WithMessage("Lütfen şube adını giriniz.")
                .MaximumLength(100).WithMessage("Şube adı maksimum 100 karakter uzunluğunda olabilir.")
                .Must(FunctionsValidator.BeUniqueSubeAdi).WithMessage("Girilen şube adı zaten sistemde kayıtlı.");

            RuleFor(x => x.TelefonNumarasi)
                .MaximumLength(11).WithMessage("Telefon numarası maksimum 11 karakter olabilir.")
                .NotEmpty().WithMessage("Lütfen telefon numarasını giriniz.")
                .NotNull().WithMessage("Lütfen telefon numarasını giriniz.")
                .Matches(@"^0\d{10}$").WithMessage("Telefon numarası geçersiz.");

            RuleFor(x => x.Sehir)
                .NotEmpty().WithMessage("Lütfen şehri giriniz.")
                .NotNull().WithMessage("Lütfen şehri giriniz.")
                .MaximumLength(50).WithMessage("Şehir adı maksimum 50 karakter uzunluğunda olabilir.");

            RuleFor(x => x.Ilce)
                .NotEmpty().WithMessage("Lütfen ilçeyi giriniz.")
                .NotNull().WithMessage("Lütfen ilçeyi giriniz.")
                .MaximumLength(100).WithMessage("İlçe adı maksimum 100 karakter uzunluğunda olabilir.");

            RuleFor(x => x.Mahalle)
                .NotEmpty().WithMessage("Lütfen mahalleyi giriniz.")
                .NotNull().WithMessage("Lütfen mahalleyi giriniz.")
                .MaximumLength(100).WithMessage("Mahalle adı maksimum 100 karakter uzunluğunda olabilir.");

            RuleFor(x => x.Cadde)
                .NotEmpty().WithMessage("Lütfen caddeyi giriniz.")
                .NotNull().WithMessage("Lütfen caddeyi giriniz.")
                .MaximumLength(100).WithMessage("Cadde adı maksimum 100 karakter uzunluğunda olabilir.");

            RuleFor(x => x.Sokak)
                .NotEmpty().WithMessage("Lütfen sokağı giriniz.")
                .NotNull().WithMessage("Lütfen sokağı giriniz.")
                .MaximumLength(100).WithMessage("Sokak adı maksimum 100 karakter uzunluğunda olabilir.");

            RuleFor(x => x.No)
                .NotEmpty().WithMessage("Lütfen no giriniz.")
                .NotNull().WithMessage("Lütfen no giriniz.")
                .MaximumLength(20).WithMessage("No maksimum 20 karakter uzunluğunda olabilir.");

        }
    }
}
